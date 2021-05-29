using Microsoft.AspNet.Identity;
using Rotativa;
using ScientificReport.DAL;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ScientificReport.DAL.Abstraction;
using ScientificReport.Services.Abstraction;
using UserManagement.Converter;
using UserManagement.Models.Reports;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class ReportsCathedraController : Controller
    {
        private ICathedraReportService cathedraReportService;
        private IUnitOfWork db;
        public ReportsCathedraController(ICathedraReportService cathedraReportService, IUnitOfWork db)
        {
            this.cathedraReportService = cathedraReportService;
            this.db = db;
        }

        // GET: Report
        public ActionResult Index(int? stepIndex, int? reportId)
        {
            ViewBag.stepIndex = stepIndex ?? 0;
            var reportVerifiedId = reportId ?? -1;

            var currentUser = db.Users.GetAllAsync().Result.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var allReports = db.Reports.GetAllAsync().Result.Where(x => x.User.Cathedra.Id == currentUser.Cathedra.Id && x.IsSigned && x.IsConfirmed && x.ThemeOfScientificWork != null).ToList();
            List<Report> lectorsReports = new List<Report>();
            if(stepIndex==0)
            {
                lectorsReports = allReports.Where(x => x.ThemeOfScientificWork.Financial == Financial.БЮДЖЕТ).ToList();
            }
            else if(stepIndex == 1)
            {
                lectorsReports = allReports.Where(x => x.ThemeOfScientificWork.Financial == Financial.В_МЕЖАХ_РОБОЧОГО_ЧАСУ).ToList();
            }
            else if(stepIndex==2)
            {
                lectorsReports = allReports.Where(x => x.ThemeOfScientificWork.Financial == Financial.ГОСПДОГОВІР).ToList();
            }
            ViewBag.AllThemeDescriptions = lectorsReports
               .GroupBy(x => x.ThemeOfScientificWork.Id).ToDictionary(k => k.Key.ToString(), v => v.Select(y => y.ThemeOfScientificWorkDescription).ToList());

            var themes = lectorsReports.Select(x => x.ThemeOfScientificWork).ToList();
            ViewBag.ScientificThemesByFaculty = themes.Select(x => {
                var text = x.Financial == Financial.БЮДЖЕТ
                ? $"{x.Code} {x.Value}"
                : (x.Financial == Financial.В_МЕЖАХ_РОБОЧОГО_ЧАСУ)
                ? $"{x.ScientificHead} {x.Value}"
                : x.Value;

                return new SelectListItem
                {
                    Text = text,
                    Value = x.Id.ToString(),
                };
            }).ToList();
            CathedraReport oldReport;
            oldReport = reportVerifiedId == -1
                ? db.CathedraReports.GetAllAsync().Result.FirstOrDefault(x => x.User.UserName == User.Identity.Name)
                : db.CathedraReports.FindByIdAsync(reportVerifiedId).Result;
            var allPublications = new List<Publication>();
            foreach (var r in lectorsReports)
            {
                allPublications.AddRange(r.PrintedPublication);
            }
            allPublications = allPublications.Distinct().ToList();
            if (oldReport != null)
            {
                return ChooseOldReport(oldReport, allPublications);
            }

            var publicationOptions = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = true,
                        Id = x.Id,
                        Name = x.Name
                    };
                    return option;
                })
                .ToList();
           return View(new CathedraReportViewModel()
            {
                Id = oldReport?.Id,
                PrintedPublicationBudgetTheme = publicationOptions,
                PrintedPublicationHospDohovirTheme = publicationOptions,
                PrintedPublicationThemeInWorkTime = publicationOptions,
                Patents = String.Join("\n\r", lectorsReports.Select(x => x.PatentForInevention).ToList()),
                ApplicationOnInvention = String.Join("\n\r", lectorsReports.Select(x => x.ApplicationForInevention).ToList())
           });
        }

        [HttpPost]
        public ActionResult Update(CathedraReportViewModel reportViewModel, int? stepIndex, int? oldIndex)
        {
            CreateOrUpdateReport(reportViewModel, oldIndex ?? 0);
            return RedirectToAction("Index", new { stepIndex = stepIndex, reportId = reportViewModel.Id });
        }

        public ActionResult Preview(int reportId)
        {
            return Content(cathedraReportService.GenerateHTMLReport(reportId));
        }

        public ActionResult PreviewPdf(int reportId)
        {
            return new ActionAsPdf("Preview", new { reportId = reportId });
        }

        public ActionResult GetLatex(int reportId)
        {
            String content = cathedraReportService.GenerateHTMLReport(reportId);
            var file = Path.Combine(ConfigurationManager.AppSettings["HtmlFilePath"], @"test.html");
            System.IO.File.WriteAllText(file, content);
            String result = "";
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(ConfigurationManager.AppSettings["PandocPath"], "pandoc.exe"),
                    Arguments = $@"--from html {file} --to latex -s --wrap=preserve",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    StandardOutputEncoding = System.Text.Encoding.GetEncoding(866)
                }
            };
            proc.Start();
            int i = 0;
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                result += line;
                result += "\n";
                i++;
                if (i == 8)
                {
                    result += @"\usepackage[ukrainian]{babel}" + "\n";
                }
            }

            return File(System.Text.Encoding.GetEncoding(866).GetBytes(result), "application/x-latex", "report.tex");
        }

        private async void CreateOrUpdateReport(CathedraReportViewModel reportViewModel, int stepIndex)
        {
            var allPublications = db.Publications.GetAllAsync().Result.ToList();
            if (reportViewModel.Id == null && db.Reports.GetAllAsync().Result.All(x => x.Id != reportViewModel.Id))
            {
                var reportToCreate = ReportConverter.ConvertToEntity(reportViewModel);
                reportToCreate.User = db.Users.FindByIdAsync(User.Identity.GetUserId()).Result;

                reportToCreate.BudgetTheme = db.ThemeOfScientificWorks.GetAllAsync().Result.FirstOrDefault(x => x.Id == reportViewModel.BudgetThemeId);
                if (reportViewModel.PrintedPublicationBudgetTheme != null)
                    reportToCreate.PrintedPublicationBudgetTheme = allPublications
                        .Where(x => reportViewModel.PrintedPublicationBudgetTheme.Any(y => y.Id == x.Id && y.Checked)).ToList();
                    
                await db.CathedraReports.CreateAsync(reportToCreate);
                db.SaveChanges();
            }
            else
            {
                var report = await db.CathedraReports.FindByIdAsync(reportViewModel.Id.Value);
                switch (stepIndex)
                {
                    case 0:
                        report.BudgetTheme = db.ThemeOfScientificWorks.GetAllAsync().Result.FirstOrDefault(x => x.Id == reportViewModel.BudgetThemeId);
                        if (reportViewModel.PrintedPublicationBudgetTheme != null)
                            report.PrintedPublicationBudgetTheme = allPublications
                            .Where(x => reportViewModel.PrintedPublicationBudgetTheme.Any(y => y.Id == x.Id && y.Checked)).ToList();
                        report.AllDescriptionBudgetTheme = reportViewModel.AllDescriptionBudgetTheme;
                        report.CVBudgetTheme = reportViewModel.CVBudgetTheme;
                        report.ApplicationAndPatentsOnInventionBudgetTheme = reportViewModel.ApplicationAndPatentsOnInventionBudgetTheme;
                        report.OtherBudgetTheme = reportViewModel.OtherBudgetTheme;
                        report.DefensesOfCoworkersBudgetTheme = reportViewModel.DefensesOfCoworkersBudgetTheme;
                        break;
                    case 1:
                        report.ThemeInWorkTime = db.ThemeOfScientificWorks.GetAllAsync().Result.FirstOrDefault(x => x.Id == reportViewModel.ThemeInWorkTimeId);
                        if (reportViewModel.PrintedPublicationThemeInWorkTime != null)
                            report.PrintedPublicationThemeInWorkTime = allPublications
                            .Where(x => reportViewModel.PrintedPublicationThemeInWorkTime.Any(y => y.Id == x.Id && y.Checked)).ToList();
                        report.AllDescriptionThemeInWorkTime = reportViewModel.AllDescriptionThemeInWorkTime;
                        report.CVThemeInWorkTime = reportViewModel.CVThemeInWorkTime;
                        report.ApplicationAndPatentsOnInventionThemeInWorkTime = reportViewModel.ApplicationAndPatentsOnInventionThemeInWorkTime;
                        report.OtherThemeInWorkTime = reportViewModel.OtherThemeInWorkTime;
                        report.DefensesOfCoworkersThemeInWorkTime = reportViewModel.DefensesOfCoworkersThemeInWorkTime;
                        break;
                    case 2:
                        report.HospDohovirTheme = db.ThemeOfScientificWorks.GetAllAsync().Result.FirstOrDefault(x => x.Id == reportViewModel.HospDohovirThemeId);
                        if (reportViewModel.PrintedPublicationHospDohovirTheme != null)
                            report.PrintedPublicationHospDohovirTheme = allPublications
                            .Where(x => reportViewModel.PrintedPublicationHospDohovirTheme.Any(y => y.Id == x.Id && y.Checked)).ToList();
                        report.AllDescriptionHospDohovirTheme = reportViewModel.AllDescriptionHospDohovirTheme;
                        report.CVHospDohovirTheme = reportViewModel.CVHospDohovirTheme;
                        report.ApplicationAndPatentsOnInventionHospDohovirTheme = reportViewModel.ApplicationAndPatentsOnInventionHospDohovirTheme;
                        report.OtherHospDohovirTheme = reportViewModel.OtherHospDohovirTheme;
                        report.DefensesOfCoworkersHospDohovirTheme = reportViewModel.DefensesOfCoworkersHospDohovirTheme;
                        break;
                    case 3:
                        report.AchivementSchool = reportViewModel.AchivementSchool;
                        report.OtherFormsOfScientificWork = reportViewModel.OtherFormsOfScientificWork;
                        report.CooperationWithAcadamyOfScience = reportViewModel.CooperationWithAcadamyOfScience;
                        report.CooperationWithForeignScientificInstitution = reportViewModel.CooperationWithForeignScientificInstitution;
                        report.StudentsWorks = reportViewModel.StudentsWorks;
                        report.ConferencesInUniversity = reportViewModel.ConferencesInUniversity;
                        report.ApplicationOnInvention = reportViewModel.ApplicationOnInvention;
                        report.Patents = reportViewModel.Patents;
                        report.Materials = reportViewModel.Materials;
                        report.PropositionForNewForms = reportViewModel.PropositionForNewForms;
                        break;
                    case 4:
                        report.Date = reportViewModel.Date;
                        report.Protocol = reportViewModel.Protocol;
                        break;
                    default:
                        return;
                }
                db.SaveChanges();
            }
        }

        private ActionResult ChooseOldReport(CathedraReport oldReport, List<Publication> allPublications)
        {
            var viewModel = ReportConverter.ConvertToViewModel(oldReport);
            viewModel.PrintedPublicationBudgetTheme = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.Id,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublicationBudgetTheme.Any(y => y.Id == x.Id))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            viewModel.PrintedPublicationHospDohovirTheme = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.Id,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublicationHospDohovirTheme.Any(y => y.Id == x.Id))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            viewModel.PrintedPublicationThemeInWorkTime = allPublications
                .Select(x =>
                {
                    var option = new PublicationOption()
                    {
                        Checked = false,
                        Id = x.Id,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublicationThemeInWorkTime.Any(y => y.Id == x.Id))
                    {
                        option.Checked = true;
                    }
                    return option;
                })
                .ToList();
            return View(viewModel);
        }
    }
}