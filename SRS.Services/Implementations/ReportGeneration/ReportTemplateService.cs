using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications.PublicationSpecifications;
using SRS.Domain.Specifications.UserSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Extensions;
using SRS.Services.Interfaces.Bibliography;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.ReportGenerationModels.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Services.Implementations.ReportGeneration
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IBaseRepository<Report> _repo;
        private readonly IBaseRepository<Publication> _publicationRepository;
        private readonly IUserRepository _userRepo;
        private readonly IBibliographyService<Publication> _publicationBibliographyService;
        private readonly IBibliographyService<ThemeOfScientificWork> _themeBibliographyService;

        public ReportTemplateService(
            IBaseRepository<Report> repo,
            IBaseRepository<Publication> publicationRepository,
            IUserRepository userRepo,
            IBibliographyService<Publication> publicationBibliographyService,
            IBibliographyService<ThemeOfScientificWork> themeBibliographyService)
        {
            _repo = repo;
            _publicationRepository = publicationRepository;
            _userRepo = userRepo;
            _publicationBibliographyService = publicationBibliographyService;
            _themeBibliographyService = themeBibliographyService;
        }

        public async Task<ReportTemplateModel> BuildAsync(int reportId)
        {
            var dbReport = await _repo.GetAsync(reportId);
            var cathedraLeads = await _userRepo.GetAsync(new CathedraLeadSpecification(dbReport.User.CathedraId));
            var allPrintedPublications = await _publicationRepository.GetAsync(new UserPrintedPublicationSpecification(dbReport.UserId));
            var allPatents = await _publicationRepository.GetAsync(new UserPatentSpecification(dbReport.UserId));
            var report = new ReportTemplateModel();
            report.GeneralInfo = GetGeneralInfo(dbReport);
            report.UserInfo = GetUserInfo(dbReport);
            report.ScientificWork = GetScientificWorkInfo(dbReport);
            report.PublicationCounters = GetPublicationCounters(dbReport, allPrintedPublications, allPatents);
            report.Publications = GetPublications(dbReport);
            report.Signature = GetSignature(dbReport, cathedraLeads);
            return report;
        }

        private ReportGeneralInfoModel GetGeneralInfo(Report dbReport)
        {
            var generalInfo = new ReportGeneralInfoModel();
            generalInfo.Year = dbReport.Date?.Year ?? 0;
            generalInfo.ApplicationForInevention = dbReport.ApplicationForInevention;
            generalInfo.PatentForInevention = dbReport.PatentForInevention;
            generalInfo.ReviewForTheses = dbReport.ReviewForTheses;
            generalInfo.MembershipInCouncils = dbReport.MembershipInCouncils;
            generalInfo.Other = dbReport.Other;
            return generalInfo;
        }

        private ReportUserInfoModel GetUserInfo(Report dbReport)
        {
            var userInfo = new ReportUserInfoModel();
            var currentYear = dbReport.Date?.Year ?? DateTime.Now.Year;
            userInfo.Position = GetCorrectedPosition(dbReport.State != ReportState.Draft ? dbReport.PositionName : dbReport.User.Position?.Value);
            userInfo.Cathedra = (dbReport.State != ReportState.Draft ? dbReport.CathedraName : dbReport.User.Cathedra?.GenitiveCase).TransformFirstLetter(char.ToLower);
            userInfo.UserFullName = dbReport.State != ReportState.Draft ? dbReport.UserFullName : dbReport.User.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
            userInfo.BirthYear = dbReport.User.BirthDate.Year;
            userInfo.GraduationYear = dbReport.User.GraduationDate?.Year ?? 0;
            userInfo.AspStart = dbReport.User.AspirantStartYear?.Year <= currentYear ? dbReport.User.AspirantStartYear?.Year : null;
            userInfo.AspFinish = dbReport.User.AspirantFinishYear?.Year <= currentYear ? dbReport.User.AspirantFinishYear?.Year : null;
            userInfo.DocStart = dbReport.User.DoctorStartYear?.Year <= currentYear ? dbReport.User.DoctorStartYear?.Year : null;
            userInfo.DocFinish = dbReport.User.DoctorFinishYear?.Year <= currentYear ? dbReport.User.DoctorFinishYear?.Year : null;
            userInfo.ScopusHIndex = dbReport.State != ReportState.Draft ? dbReport.ScopusHIndex : dbReport.User.ScopusHIndex;
            userInfo.WebOfScienceHIndex = dbReport.State != ReportState.Draft ? dbReport.WebOfScienceHIndex : dbReport.User.WebOfScienceHIndex;
            userInfo.GoogleScholarHIndex = dbReport.State != ReportState.Draft ? dbReport.GoogleScholarHIndex : dbReport.User.GoogleScholarHIndex;
            userInfo.Degrees = dbReport.User.Degrees
                .Where(x => x.AwardDate.Year <= currentYear)
                .Select(degree => new ReportUserTitleModel
                {
                    Title = degree.Degree.Value.TransformFirstLetter(char.ToLower),
                    Year = degree.AwardDate.Year
                }).ToList();
            userInfo.AcademicStatuses = dbReport.User.AcademicStatuses
                .Where(x => x.AwardDate.Year <= currentYear)
                .Select(academiStatus => new ReportUserTitleModel
                {
                    Title = academiStatus.AcademicStatus.Value.TransformFirstLetter(char.ToLower),
                    Year = academiStatus.AwardDate.Year
                }).ToList();
            userInfo.HonoraryTitles = dbReport.User.HonoraryTitles
                .Where(x => x.AwardDate.Year <= currentYear)
                .Select(academiStatus => new ReportUserTitleModel
                {
                    Title = academiStatus.HonoraryTitle.Value.TransformFirstLetter(char.ToLower),
                    Year = academiStatus.AwardDate.Year
                }).ToList();
            return userInfo;
        }

        private string GetCorrectedPosition(string position)
        {
            return position?
                .Replace("кафедри", string.Empty)
                .Replace("лабораторії", string.Empty)
                .Replace("центру", string.Empty)
                .Replace("гербарію", string.Empty);
        }

        private ReportScientificWorkModel GetScientificWorkInfo(Report dbReport)
        {
            var scientificWorkInfo = new ReportScientificWorkModel();
            scientificWorkInfo.ThemeOfScientificWorks = GetThemeOfScientificWorks(dbReport);
            scientificWorkInfo.OtherThemeOfScientificWorkDescription = dbReport.OtherThemeOfScientificWorkDescription;
            scientificWorkInfo.OtherGrantDescription = dbReport.OtherGrantDescription;
            scientificWorkInfo.Grants = GetGrants(dbReport);
            scientificWorkInfo.ScientificTrainings = dbReport.ScientificTrainings;
            scientificWorkInfo.ScientificControlDoctorsWork = dbReport.ScientificControlDoctorsWork;
            scientificWorkInfo.ScientificControlStudentsWork = dbReport.ScientificControlStudentsWork;
            scientificWorkInfo.StudentPublications = GetPublicationsBibliography(dbReport.StudentPublication.ToList());
            return scientificWorkInfo;
        }

        private List<ReportThemeOfScientificWorkModel> GetThemeOfScientificWorks(Report dbReport)
        {
            var result = new List<ReportThemeOfScientificWorkModel>();
            var themes = dbReport.ThemeOfScientificWorks.Where(x => x.ThemeOfScientificWork.Financial != Financial.InternationalGrant);
            foreach (var theme in themes)
            {
                result.Add(ConvertToReportTheme(theme));
            }

            return result;
        }

        private List<ReportThemeOfScientificWorkModel> GetGrants(Report dbReport)
        {
            var result = new List<ReportThemeOfScientificWorkModel>();
            var grants = dbReport.ThemeOfScientificWorks.Where(x => x.ThemeOfScientificWork.Financial == Financial.InternationalGrant);
            foreach (var grant in grants)
            {
                result.Add(ConvertToReportTheme(grant));
            }

            return result;
        }

        private ReportThemeOfScientificWorkModel ConvertToReportTheme(ReportThemeOfScientificWork theme)
        {
            var themeOfScientificWork = new ReportThemeOfScientificWorkModel();
            themeOfScientificWork.Theme = _themeBibliographyService.Get(theme.ThemeOfScientificWork);
            themeOfScientificWork.Description = theme.Description;
            return themeOfScientificWork;
        }

        private ReportPublicationCountersModel GetPublicationCounters(Report dbReport, List<Publication> allPrintedPublications, List<Publication> allPatents)
        {
            var reportPrintedPublications = dbReport.PrintedPublication.ToList();
            var publicationCounters = new ReportPublicationCountersModel();
            publicationCounters.MonographsAllCount = dbReport.User.MonographCounterBeforeRegistration + allPrintedPublications.Count(x => x.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві);
            publicationCounters.MonographsPeriodCount = reportPrintedPublications.Count(x => x.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві);
            publicationCounters.BooksAllCount = dbReport.User.BookCounterBeforeRegistration + allPrintedPublications.Count(x => x.PublicationType == PublicationType.Підручник);
            publicationCounters.BooksPeriodCount = reportPrintedPublications.Count(x => x.PublicationType == PublicationType.Підручник);
            publicationCounters.TrainingBooksAllCount = dbReport.User.TrainingBookCounterBeforeRegistration + allPrintedPublications.Count(x => x.PublicationType == PublicationType.Навчальний_Посібник);
            publicationCounters.TrainingBooksPeriodCount = reportPrintedPublications.Count(x => x.PublicationType == PublicationType.Навчальний_Посібник);
            publicationCounters.ArticlesAllCount = dbReport.User.PublicationCounterBeforeRegistration + allPrintedPublications.Count(x => x.IsArticle());
            publicationCounters.ArticlesPeriodCount = reportPrintedPublications.Count(x => x.IsArticle());
            publicationCounters.OtherWritingsAllCount = dbReport.User.OtherWritingCounterBeforeRegistration + allPrintedPublications.Count(x => x.PublicationType == PublicationType.Інше_Наукове_Видання);
            publicationCounters.OtherWritingsPeriodCount = reportPrintedPublications.Count(x => x.PublicationType == PublicationType.Інше_Наукове_Видання);
            publicationCounters.ConferencesAllCount = dbReport.User.ConferenceCounterBeforeRegistration + allPrintedPublications.Count(x => x.IsConference());
            publicationCounters.ConferencesPeriodCount = reportPrintedPublications.Count(x => x.IsConference());
            publicationCounters.PatentsAllCount = dbReport.User.PatentCounterBeforeRegistration + allPatents.Count(x => x.PublicationType == PublicationType.Патент);
            publicationCounters.PatentsPeriodCount = dbReport.PatentsForInvention.Count(x => x.PublicationType == PublicationType.Патент);
            return publicationCounters;
        }

        private ReportPublicationsModel GetPublications(Report dbReport)
        {
            var reportPrintedPublications = dbReport.PrintedPublication.ToList();
            var reportRecommendedPublications = dbReport.RecomendedPublication.ToList();
            var reportAcceptedToPrintPublications = dbReport.AcceptedToPrintPublication.ToList();
            var reportApplicationsForInvention = dbReport.ApplicationsForInvention.ToList();
            var reportPatentsForInvention = dbReport.PatentsForInvention.ToList();
            var publications = new ReportPublicationsModel();
            publications.Monographs = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві));
            publications.Books = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Підручник));
            publications.TrainingBooks = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Навчальний_Посібник));
            publications.OtherWritings = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Інше_Наукове_Видання));
            publications.ImpactFactorArticles = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор));
            publications.InternationalMetricArticles = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних));
            publications.OtherInternationalArticles = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Закордонних_Виданнях));
            publications.NationalProfessionalArticles = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Фахових_Виданнях_України));
            publications.OtherNationalArticles = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_України));
            publications.InternationalConferences = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Міжнародній_Конференції));
            publications.NationalConferences = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції));
            publications.RecommendedPublications = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType != PublicationType.Монографія_У_Закордонному_Видавництві
                && x.PublicationType != PublicationType.Монографія_У_Вітчизняному_Видавництві
                && x.PublicationType != PublicationType.Розділ_монографії_У_Закордонному_Видавництві
                && x.PublicationType != PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві
                && x.PublicationType != PublicationType.Підручник
                && x.PublicationType != PublicationType.Навчальний_Посібник
                && x.PublicationType != PublicationType.Інше_Наукове_Видання));
            publications.RecommendedMonographs = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Монографія_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Монографія_У_Вітчизняному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Закордонному_Видавництві
                || x.PublicationType == PublicationType.Розділ_монографії_У_Вітчизняному_Видавництві));
            publications.RecommendedBooks = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Підручник));
            publications.RecommendedTrainingBooks = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Навчальний_Посібник));
            publications.RecommendedOtherWritings = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Інше_Наукове_Видання));
            publications.AcceptedToPrintPublications = GetPublicationsBibliography(reportAcceptedToPrintPublications);
            publications.ApplicationsForInvention = GetPublicationsBibliography(reportApplicationsForInvention);
            publications.PatentsForInvention = GetPublicationsBibliography(reportPatentsForInvention);
            return publications;
        }

        private ReportSignatureModel GetSignature(Report dbReport, List<ApplicationUser> cathedraLeads)
        {
            var cathedraLead = cathedraLeads.FirstOrDefault();
            var signature = new ReportSignatureModel();
            signature.Protocol = dbReport.Protocol;
            signature.Date = dbReport.Date?.ToString("dd.MM.yyyy");
            signature.CathedraLead = dbReport.State != ReportState.Draft ? dbReport.CathedraLeadName : cathedraLead?.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.ShortReverseFullName;
            return signature;
        }

        private List<string> GetPublicationsBibliography(IEnumerable<Publication> publications)
        {
            return publications.Select(x => _publicationBibliographyService.Get(x)).ToList();
        }
    }
}
