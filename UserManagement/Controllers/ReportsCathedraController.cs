﻿using Microsoft.AspNet.Identity;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using UserManagement.Converter;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Models.Reports;
using UserManagement.Services;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Керівник кафедри")]
    public class ReportsCathedraController : Controller
    {
        private ApplicationDbContext db;
        private CathedraReportService cathedraReportService;

        public ReportsCathedraController()
        {
            db = new ApplicationDbContext();
            cathedraReportService = new CathedraReportService(db);
        }

        // GET: Report
        public ActionResult Index(int? stepIndex, int? reportId)
        {
            db = new ApplicationDbContext();
            ViewBag.stepIndex = stepIndex ?? 0;
            var reportVerifiedId = reportId ?? -1;

            var currentUser = db.Users.First(x => x.UserName == User.Identity.Name);
            var allReports = db.Reports.Include(x=>x.ThemeOfScientificWork)
                .Where(x => x.User.Cathedra.ID == currentUser.Cathedra.ID && x.IsSigned && x.IsConfirmed && x.ThemeOfScientificWork != null).ToList();
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
               .GroupBy(x => x.ThemeOfScientificWork.ID).ToDictionary(k => k.Key.ToString(), v => new List<string> { v.FirstOrDefault()?.ThemeOfScientificWorkDescription });

            var themes = lectorsReports.GroupBy(x => x.ThemeOfScientificWork).Select(x=>x.Key).ToList();
            ViewBag.ScientificThemesByFaculty = themes.Select(x => {
                var text = x.Financial == Financial.БЮДЖЕТ
                ? $"{x.Code} {x.Value}"
                : (x.Financial == Financial.В_МЕЖАХ_РОБОЧОГО_ЧАСУ)
                ? $"{x.ScientificHead} {x.Value}"
                : x.Value;

                return new SelectListItem
                {
                    Text = text,
                    Value = x.ID.ToString(),
                };
            }).ToList();
            CathedraReport oldReport;
            if (reportVerifiedId == -1)
            {
                oldReport = db.CathedraReport.Where(x => x.User.UserName == User.Identity.Name).FirstOrDefault();
            }
            else
            {
                oldReport = db.CathedraReport.Find(reportVerifiedId);
            }
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
                        Id = x.ID,
                        Name = x.Name
                    };
                    return option;
                })
                .ToList();
           return View(new CathedraReportViewModel()
            {
                ID = oldReport?.ID,
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
            return RedirectToAction("Index", new { stepIndex = stepIndex, reportId = reportViewModel.ID });
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

        private void CreateOrUpdateReport(CathedraReportViewModel reportViewModel, int stepIndex)
        {
            var allPublications = db.Publication.ToList();
            if (reportViewModel.ID == null && !db.Reports.Any(x => x.ID == reportViewModel.ID))
            {
                var reportToCreate = ReportConverter.ConvertToEntity(reportViewModel);
                reportToCreate.User = db.Users.Find(User.Identity.GetUserId());

                reportToCreate.BudgetTheme = db.ThemeOfScientificWork.Where(x => x.ID == reportViewModel.BudgetThemeId).FirstOrDefault();
                if (reportViewModel.PrintedPublicationBudgetTheme != null)
                    reportToCreate.PrintedPublicationBudgetTheme = allPublications
                        .Where(x => reportViewModel.PrintedPublicationBudgetTheme.Any(y => y.Id == x.ID && y.Checked)).ToList();
                    
                db.CathedraReport.Add(reportToCreate);
                db.SaveChanges();
            }
            else
            {
                var report = db.CathedraReport.Find(reportViewModel.ID);
                switch (stepIndex)
                {
                    case 0:
                        report.BudgetTheme = db.ThemeOfScientificWork.Where(x => x.ID == reportViewModel.BudgetThemeId).FirstOrDefault();
                        if (reportViewModel.PrintedPublicationBudgetTheme != null)
                        {
                            report.PrintedPublicationBudgetTheme.Clear();
                            report.PrintedPublicationBudgetTheme = allPublications
                            .Where(x => reportViewModel.PrintedPublicationBudgetTheme.Any(y => y.Id == x.ID && y.Checked)).ToList();
                        }
                        report.AllDescriptionBudgetTheme = reportViewModel.AllDescriptionBudgetTheme;
                        report.CVBudgetTheme = reportViewModel.CVBudgetTheme;
                        report.ApplicationAndPatentsOnInventionBudgetTheme = reportViewModel.ApplicationAndPatentsOnInventionBudgetTheme;
                        report.OtherBudgetTheme = reportViewModel.OtherBudgetTheme;
                        report.DefensesOfCoworkersBudgetTheme = reportViewModel.DefensesOfCoworkersBudgetTheme;
                        break;
                    case 1:
                        report.ThemeInWorkTime = db.ThemeOfScientificWork.Where(x => x.ID == reportViewModel.ThemeInWorkTimeId).FirstOrDefault();
                        if (reportViewModel.PrintedPublicationThemeInWorkTime != null)
                        {
                            report.PrintedPublicationThemeInWorkTime.Clear();
                            report.PrintedPublicationThemeInWorkTime = allPublications
                            .Where(x => reportViewModel.PrintedPublicationThemeInWorkTime.Any(y => y.Id == x.ID && y.Checked)).ToList();
                        }
                        report.AllDescriptionThemeInWorkTime = reportViewModel.AllDescriptionThemeInWorkTime;
                        report.CVThemeInWorkTime = reportViewModel.CVThemeInWorkTime;
                        report.ApplicationAndPatentsOnInventionThemeInWorkTime = reportViewModel.ApplicationAndPatentsOnInventionThemeInWorkTime;
                        report.OtherThemeInWorkTime = reportViewModel.OtherThemeInWorkTime;
                        report.DefensesOfCoworkersThemeInWorkTime = reportViewModel.DefensesOfCoworkersThemeInWorkTime;
                        break;
                    case 2:
                        report.HospDohovirTheme = db.ThemeOfScientificWork.Where(x => x.ID == reportViewModel.HospDohovirThemeId).FirstOrDefault();
                        if (reportViewModel.PrintedPublicationHospDohovirTheme != null)
                        {
                            report.PrintedPublicationHospDohovirTheme.Clear();
                            report.PrintedPublicationHospDohovirTheme = allPublications
                            .Where(x => reportViewModel.PrintedPublicationHospDohovirTheme.Any(y => y.Id == x.ID && y.Checked)).ToList();
                        }
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
                        Id = x.ID,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublicationBudgetTheme.Any(y => y.Id == x.ID))
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
                        Id = x.ID,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublicationHospDohovirTheme.Any(y => y.Id == x.ID))
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
                        Id = x.ID,
                        Name = x.Name
                    };
                    if (viewModel.PrintedPublicationThemeInWorkTime.Any(y => y.Id == x.ID))
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