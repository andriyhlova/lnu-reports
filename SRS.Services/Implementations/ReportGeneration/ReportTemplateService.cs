using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications.PublicationSpecifications;
using SRS.Domain.Specifications.UserSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Extensions;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.ReportGenerationModels.Report;

namespace SRS.Services.Implementations.ReportGeneration
{
    public class ReportTemplateService : IReportTemplateService
    {
        private readonly IBaseRepository<Report> _repo;
        private readonly IBaseRepository<Publication> _publicationRepository;
        private readonly IUserRepository _userRepo;
        private readonly IBibliographyService _bibliographyService;

        public ReportTemplateService(
            IBaseRepository<Report> repo,
            IBaseRepository<Publication> publicationRepository,
            IUserRepository userRepo,
            IBibliographyService bibliographyService)
        {
            _repo = repo;
            _publicationRepository = publicationRepository;
            _userRepo = userRepo;
            _bibliographyService = bibliographyService;
        }

        public async Task<ReportTemplateModel> BuildAsync(int reportId)
        {
            var dbReport = await _repo.GetAsync(reportId);
            var cathedraLeads = await _userRepo.GetAsync(new CathedraLeadSpecification(dbReport.User.CathedraId));
            var allPrintedPublications = await _publicationRepository.GetAsync(new UserPrintedPublicationSpecification(dbReport.UserId));
            var report = new ReportTemplateModel();
            report.GeneralInfo = GetGeneralInfo(dbReport);
            report.UserInfo = GetUserInfo(dbReport);
            report.ThemeOfScientificWorks = GetThemeOfScientificWorks(dbReport);
            report.PublicationCounters = GetPublicationCounters(dbReport, allPrintedPublications);
            report.Publications = GetPublications(dbReport);
            report.Signature = GetSignature(dbReport, cathedraLeads);
            return report;
        }

        private ReportGeneralInfoModel GetGeneralInfo(Report dbReport)
        {
            var generalInfo = new ReportGeneralInfoModel();
            generalInfo.Year = dbReport.Date?.Year ?? 0;
            generalInfo.ThemeOfScientificWorkDescription = dbReport.ThemeOfScientificWorkDescription;
            generalInfo.ParticipationInGrands = dbReport.ParticipationInGrands;
            generalInfo.ScientificTrainings = dbReport.ScientificTrainings;
            generalInfo.ScientificControlDoctorsWork = dbReport.ScientificControlDoctorsWork;
            generalInfo.ScientificControlStudentsWork = dbReport.ScientificControlStudentsWork;
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
            userInfo.Position = dbReport.User.Position?.Value.Replace("кафедри", string.Empty);
            userInfo.Cathedra = dbReport.User.Cathedra?.Name.Replace("Кафедра ", string.Empty);
            userInfo.UserFullName = dbReport.User.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
            userInfo.BirthYear = dbReport.User.BirthDate.Year;
            userInfo.GraduationYear = dbReport.User.GraduationDate?.Year ?? 0;
            userInfo.AspStart = dbReport.User.AspirantStartYear?.Year;
            userInfo.AspFinish = dbReport.User.AspirantFinishYear?.Year;
            userInfo.DocStart = dbReport.User.DoctorStartYear?.Year;
            userInfo.DocFinish = dbReport.User.DoctorFinishYear?.Year;
            userInfo.ScientificDegree = dbReport.User.Degree?.Value;
            userInfo.ScientificDegreeYear = dbReport.User.DefenseYear?.Year;
            userInfo.AcademicStatus = dbReport.User.AcademicStatus?.Value;
            userInfo.AcademicStatusYear = dbReport.User.AwardingDate?.Year;
            return userInfo;
        }

        private List<ReportThemeOfScientificWorkModel> GetThemeOfScientificWorks(Report dbReport)
        {
            var result = new List<ReportThemeOfScientificWorkModel>();
            foreach (var theme in dbReport.ThemeOfScientificWorks)
            {
                var themeOfScientificWork = new ReportThemeOfScientificWorkModel();
                themeOfScientificWork.Title = theme.Value;
                themeOfScientificWork.Number = theme.ThemeNumber;
                themeOfScientificWork.Code = theme.Code;
                themeOfScientificWork.PeriodFrom = theme.PeriodFrom.Year.ToString();
                themeOfScientificWork.PeriodTo = theme.PeriodTo.Year.ToString();
                themeOfScientificWork.Head = theme.ScientificHead;
                themeOfScientificWork.Financial = theme.Financial.GetDisplayName().ToLower();
                result.Add(themeOfScientificWork);
            }

            return result;
        }

        private ReportPublicationCountersModel GetPublicationCounters(Report dbReport, List<Publication> allPrintedPublications)
        {
            var reportPrintedPublications = dbReport.PrintedPublication.ToList();
            var publicationCounters = new ReportPublicationCountersModel();
            publicationCounters.MonographsAllCount = dbReport.User.MonographCounterBeforeRegistration + allPrintedPublications.Count(x => x.PublicationType == PublicationType.Монографія);
            publicationCounters.MonographsPeriodCount = reportPrintedPublications.Count(x => x.PublicationType == PublicationType.Монографія);
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
            publicationCounters.PatentsAllCount = dbReport.User.PatentCounterBeforeRegistration + allPrintedPublications.Count(x => x.PublicationType == PublicationType.Патент);
            publicationCounters.PatentsPeriodCount = reportPrintedPublications.Count(x => x.PublicationType == PublicationType.Патент);
            return publicationCounters;
        }

        private ReportPublicationsModel GetPublications(Report dbReport)
        {
            var reportPrintedPublications = dbReport.PrintedPublication.ToList();
            var reportRecommendedPublications = dbReport.RecomendedPublication.ToList();
            var reportAcceptedToPrintPublications = dbReport.AcceptedToPrintPublication.ToList();
            var publications = new ReportPublicationsModel();
            publications.Monographs = GetPublicationsBibliography(reportPrintedPublications.Where(x => x.PublicationType == PublicationType.Монографія));
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
            publications.RecommendedPublications = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType != PublicationType.Монографія
                && x.PublicationType != PublicationType.Підручник
                && x.PublicationType != PublicationType.Навчальний_Посібник
                && x.PublicationType != PublicationType.Інше_Наукове_Видання));
            publications.RecommendedMonographs = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Монографія));
            publications.RecommendedBooks = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Підручник));
            publications.RecommendedTrainingBooks = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Навчальний_Посібник));
            publications.RecommendedOtherWritings = GetPublicationsBibliography(reportRecommendedPublications.Where(x => x.PublicationType == PublicationType.Інше_Наукове_Видання));
            publications.AcceptedToPrintPublications = GetPublicationsBibliography(reportAcceptedToPrintPublications);
            return publications;
        }

        private ReportSignatureModel GetSignature(Report dbReport, List<ApplicationUser> cathedraLeads)
        {
            var cathedraLead = cathedraLeads.FirstOrDefault();
            var signature = new ReportSignatureModel();
            signature.Protocol = dbReport.Protocol;
            signature.Date = dbReport.Date?.ToString("dd.MM.yyyy");
            signature.CathedraLead = cathedraLead?.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.ShortReverseFullName;
            signature.CathedraLeadStatus = cathedraLead?.AcademicStatus?.Value;
            return signature;
        }

        private List<string> GetPublicationsBibliography(IEnumerable<Publication> publications)
        {
            return publications.Select(x => _bibliographyService.Get(x)).ToList();
        }
    }
}
