using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications.UserSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces.Bibliography;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.ReportGenerationModels.CathedraReport;

namespace SRS.Services.Implementations.ReportGeneration
{
    public class CathedraReportTemplateService : ICathedraReportTemplateService
    {
        private readonly IBaseRepository<CathedraReport> _repo;
        private readonly IUserRepository _userRepo;
        private readonly IBibliographyService<Publication> _bibliographyService;

        public CathedraReportTemplateService(
            IBaseRepository<CathedraReport> repo,
            IUserRepository userRepo,
            IBibliographyService<Publication> bibliographyService)
        {
            _repo = repo;
            _userRepo = userRepo;
            _bibliographyService = bibliographyService;
        }

        public async Task<CathedraReportTemplateModel> BuildAsync(int reportId)
        {
            var dbReport = await _repo.GetAsync(reportId);
            var facultyLeads = await _userRepo.GetAsync(new FacultyLeadSpecification(dbReport.User.Cathedra.FacultyId));
            var report = new CathedraReportTemplateModel();
            report.GeneralInfo = GetGeneralInfo(dbReport);
            report.BudgetThemeOfScientificWork = dbReport.BudgetTheme != null ? GetBudgetThemeOfScientificWork(dbReport) : null;
            report.InWorkTimeThemeOfScientificWork = dbReport.ThemeInWorkTime != null ? GetInWorkTimeThemeOfScientificWork(dbReport) : null;
            report.HospDohovirThemeOfScientificWork = dbReport.HospDohovirTheme != null ? GetHospDohovirThemeOfScientificWork(dbReport) : null;
            report.Publications = GetPublications(dbReport);
            report.Signature = GetSignature(dbReport, facultyLeads);
            return report;
        }

        private CathedraReportGeneralInfoModel GetGeneralInfo(CathedraReport dbReport)
        {
            var generalInfo = new CathedraReportGeneralInfoModel();
            generalInfo.Cathedra = dbReport.User.Cathedra.Name.Replace("Кафедра ", string.Empty);
            generalInfo.Year = dbReport.Date?.Year ?? 0;
            generalInfo.AchievementSchool = dbReport.AchivementSchool;
            generalInfo.OtherFormsOfScientificWork = dbReport.OtherFormsOfScientificWork;
            generalInfo.CooperationWithAcademyOfScience = dbReport.CooperationWithAcadamyOfScience;
            generalInfo.CooperationWithForeignScientificInstitution = dbReport.CooperationWithForeignScientificInstitution;
            generalInfo.StudentsWorks = dbReport.StudentsWorks;
            generalInfo.ConferencesInUniversity = dbReport.ConferencesInUniversity;
            generalInfo.ApplicationOnInvention = dbReport.ApplicationOnInvention;
            generalInfo.Patents = dbReport.Patents;
            generalInfo.Materials = dbReport.Materials;
            generalInfo.PropositionForNewForms = dbReport.PropositionForNewForms;
            return generalInfo;
        }

        private CathedraReportThemeOfScientificWorkModel GetBudgetThemeOfScientificWork(CathedraReport dbReport)
        {
            var themeOfScientificWork = GetThemeOfScientificWorkBase(dbReport.BudgetTheme);
            themeOfScientificWork.Description = dbReport.AllDescriptionBudgetTheme;
            themeOfScientificWork.ThemeCV = dbReport.CVBudgetTheme;
            themeOfScientificWork.DefensesOfCoworkers = dbReport.DefensesOfCoworkersBudgetTheme;
            themeOfScientificWork.PublicationsCounters = GetPublicationCounters(dbReport.PrintedPublicationBudgetTheme);
            themeOfScientificWork.ApplicationAndPatentsOnInvention = dbReport.ApplicationAndPatentsOnInventionBudgetTheme;
            themeOfScientificWork.Other = dbReport.OtherBudgetTheme;
            return themeOfScientificWork;
        }

        private CathedraReportThemeOfScientificWorkModel GetInWorkTimeThemeOfScientificWork(CathedraReport dbReport)
        {
            var themeOfScientificWork = GetThemeOfScientificWorkBase(dbReport.ThemeInWorkTime);
            themeOfScientificWork.Description = dbReport.AllDescriptionThemeInWorkTime;
            themeOfScientificWork.ThemeCV = dbReport.CVThemeInWorkTime;
            themeOfScientificWork.DefensesOfCoworkers = dbReport.DefensesOfCoworkersThemeInWorkTime;
            themeOfScientificWork.PublicationsCounters = GetPublicationCounters(dbReport.PrintedPublicationThemeInWorkTime);
            themeOfScientificWork.ApplicationAndPatentsOnInvention = dbReport.ApplicationAndPatentsOnInventionThemeInWorkTime;
            themeOfScientificWork.Other = dbReport.OtherThemeInWorkTime;
            return themeOfScientificWork;
        }

        private CathedraReportThemeOfScientificWorkModel GetHospDohovirThemeOfScientificWork(CathedraReport dbReport)
        {
            var themeOfScientificWork = GetThemeOfScientificWorkBase(dbReport.BudgetTheme);
            themeOfScientificWork.Description = dbReport.AllDescriptionHospDohovirTheme;
            themeOfScientificWork.ThemeCV = dbReport.CVHospDohovirTheme;
            themeOfScientificWork.DefensesOfCoworkers = dbReport.DefensesOfCoworkersHospDohovirTheme;
            themeOfScientificWork.PublicationsCounters = GetPublicationCounters(dbReport.PrintedPublicationHospDohovirTheme);
            themeOfScientificWork.ApplicationAndPatentsOnInvention = dbReport.ApplicationAndPatentsOnInventionHospDohovirTheme;
            themeOfScientificWork.Other = dbReport.OtherHospDohovirTheme;
            return themeOfScientificWork;
        }

        private CathedraReportThemeOfScientificWorkModel GetThemeOfScientificWorkBase(ThemeOfScientificWork scientificWork)
        {
            var themeOfScientificWork = new CathedraReportThemeOfScientificWorkModel();
            themeOfScientificWork.Title = scientificWork.Value;
            themeOfScientificWork.Number = scientificWork.ThemeNumber;
            themeOfScientificWork.Code = scientificWork.Code;
            themeOfScientificWork.PeriodFrom = scientificWork.PeriodFrom.Year.ToString();
            themeOfScientificWork.PeriodTo = scientificWork.PeriodTo.Year.ToString();
            themeOfScientificWork.Head = scientificWork.ScientificHead;
            return themeOfScientificWork;
        }

        private List<CathedraReportPublicationCountersModel> GetPublicationCounters(List<Publication> publications)
        {
            var aggregatedPublications = publications.GroupBy(x => x.PublicationType).ToDictionary(k => k.Key, x => x.Count());
            var result = new List<CathedraReportPublicationCountersModel>();
            foreach (var item in aggregatedPublications)
            {
                result.Add(new CathedraReportPublicationCountersModel
                {
                    Type = item.Key.ToString().Replace("_", " ").ToLower(),
                    Count = item.Value
                });
            }

            return result;
        }

        private CathedraReportPublicationsModel GetPublications(CathedraReport dbReport)
        {
            var distinctPublications = dbReport.PrintedPublicationBudgetTheme
                .Union(dbReport.PrintedPublicationHospDohovirTheme)
                .Union(dbReport.PrintedPublicationThemeInWorkTime)
                .GroupBy(x => x.Id)
                .Select(x => x.First())
                .ToList();

            var publications = new CathedraReportPublicationsModel();
            publications.Monographs = GetPublicationsBibliographyModels(distinctPublications.Where(x => x.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві));
            publications.Books = GetPublicationsBibliographyModels(distinctPublications.Where(x => x.PublicationType == PublicationType.Підручник));
            publications.TrainingBooks = GetPublicationsBibliographyModels(distinctPublications.Where(x => x.PublicationType == PublicationType.Навчальний_Посібник));
            publications.OtherWritings = GetPublicationsBibliographyModels(distinctPublications.Where(x => x.PublicationType == PublicationType.Інше_Наукове_Видання));
            publications.ImpactFactorArticles = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор));
            publications.InternationalMetricArticles = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних));
            publications.OtherInternationalArticles = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Закордонних_Виданнях));
            publications.NationalProfessionalArticles = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Фахових_Виданнях_України));
            publications.OtherNationalArticles = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_України));
            publications.InternationalConferences = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Міжнародній_Конференції));
            publications.NationalConferences = GetPublicationsBibliography(distinctPublications.Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції));
            return publications;
        }

        private CathedraReportSignatureModel GetSignature(CathedraReport dbReport, List<ApplicationUser> facultyLeads)
        {
            var facultyLead = facultyLeads.FirstOrDefault();
            var signature = new CathedraReportSignatureModel();
            signature.Faculty = dbReport.User.Cathedra.Faculty.Name.Replace("Факультет ", string.Empty).ToLower();
            signature.Protocol = dbReport.Protocol;
            signature.Date = dbReport.Date?.ToString("dd.MM.yyyy");
            signature.FacultyLead = facultyLead?.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.ShortReverseFullName;
            signature.FacultyLeadStatuses = facultyLead?.AcademicStatuses.Select(academicStatus => academicStatus.AcademicStatus.Value).ToList();
            return signature;
        }

        private List<CathedraReportPublicationModel> GetPublicationsBibliographyModels(IEnumerable<Publication> publications)
        {
            return publications
                .Select(x => new CathedraReportPublicationModel
                {
                    Name = _bibliographyService.Get(x),
                    Size = x.SizeOfPages
                })
                .ToList();
        }

        private List<string> GetPublicationsBibliography(IEnumerable<Publication> publications)
        {
            return publications.Select(x => _bibliographyService.Get(x)).ToList();
        }
    }
}
