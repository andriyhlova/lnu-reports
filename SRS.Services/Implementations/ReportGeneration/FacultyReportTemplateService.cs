﻿using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications.UserSpecifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Extensions;
using SRS.Services.Interfaces;
using SRS.Services.Interfaces.Bibliography;
using SRS.Services.Interfaces.ReportGeneration;
using SRS.Services.Models.Constants;
using SRS.Services.Models.ReportGenerationModels.DepartmentReport;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Services.Implementations.ReportGeneration
{
    public class FacultyReportTemplateService : IFacultyReportTemplateService
    {
        private readonly IBaseRepository<FacultyReport> _repo;
        private readonly IUserRepository _userRepo;
        private readonly IBibliographyService<Publication> _bibliographyService;
        private readonly IThemeOfScientificWorkService _themeOfScientificWorkService;
        private readonly IBibliographyService<ThemeOfScientificWork> _themeBibliographyService;
        private readonly IDissertationDefenseService _dissertationDefenseService;
        private readonly IMapper _mapper;

        public FacultyReportTemplateService(
            IBaseRepository<FacultyReport> repo,
            IUserRepository userRepo,
            IBibliographyService<Publication> bibliographyService,
            IThemeOfScientificWorkService themeOfScientificWorkService,
            IBibliographyService<ThemeOfScientificWork> themeBibliographyService,
            IDissertationDefenseService dissertationDefenseService,
            IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _bibliographyService = bibliographyService;
            _themeOfScientificWorkService = themeOfScientificWorkService;
            _themeBibliographyService = themeBibliographyService;
            _dissertationDefenseService = dissertationDefenseService;
            _mapper = mapper;
        }

        public async Task<DepartmentReportTemplateModel> BuildAsync(int reportId)
        {
            var dbReport = await _repo.GetAsync(reportId);
            var facultyLeads = await _userRepo.GetAsync(new FacultyLeadSpecification(dbReport.User.Cathedra.FacultyId));
            var report = new DepartmentReportTemplateModel();
            report.GeneralInfo = GetGeneralInfo(dbReport);
            report.Publications = GetPublications(dbReport);
            report.Signature = GetSignature(dbReport, facultyLeads);
            report.ThemeOfScientificWorks = await GetThemeOfScientificWorksAsync(dbReport);
            report.Grants = GetGrants(dbReport);
            report.OtherGrantDescription = dbReport.OtherGrants;
            return report;
        }

        private DepartmentReportGeneralInfoModel GetGeneralInfo(FacultyReport dbReport)
        {
            var generalInfo = new DepartmentReportGeneralInfoModel();
            generalInfo.Department = "Факультет";
            generalInfo.DepartmentName = dbReport.User.Cathedra.Faculty.Name.ToLower();
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
            generalInfo.DissertationDefenseOfGraduates = _mapper.Map<List<DepartmentReportDissertarionDefenseModel>>(dbReport.DissertationDefenseOfGraduates);
            generalInfo.DissertationDefenseOfEmployees = _mapper.Map<List<DepartmentReportDissertarionDefenseModel>>(dbReport.DissertationDefenseOfEmployees);
            generalInfo.DissertationDefenseInAcademicCouncil = _mapper.Map<List<DepartmentReportDissertarionDefenseModel>>(dbReport.DissertationDefenseInAcademicCouncil);
            return generalInfo;
        }

        private DepartmentReportPublicationsModel GetPublications(FacultyReport dbReport)
        {
            var distinctPublications = dbReport.Publications
                .GroupBy(x => x.Id)
                .Select(x => x.First())
                .ToList();

            var reportApplicationsForInvention = dbReport.ApplicationsForInvention.ToList();
            var reportPatentsForInvention = dbReport.PatentsForInvention.ToList();

            var publications = new DepartmentReportPublicationsModel();
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
            publications.ApplicationsForInvention = GetPublicationsBibliography(reportApplicationsForInvention);
            publications.PatentsForInvention = GetPublicationsBibliography(reportPatentsForInvention);

            return publications;
        }

        private DepartmentReportSignatureModel GetSignature(FacultyReport dbReport, List<ApplicationUser> facultyLeads)
        {
            var facultyLead = facultyLeads.FirstOrDefault();
            var signature = new DepartmentReportSignatureModel();
            signature.Faculty = dbReport.User.Cathedra.Faculty.Name.Replace("Факультет ", string.Empty).ToLower();
            signature.Protocol = dbReport.Protocol;
            signature.Date = dbReport.Date?.ToString("dd.MM.yyyy");
            signature.FacultyLead = facultyLead?.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.ShortReverseFullName;
            signature.FacultyLeadStatuses = facultyLead?.AcademicStatuses.Select(academicStatus => academicStatus.AcademicStatus.Value).ToList();
            return signature;
        }

        private List<DepartmentReportPublicationModel> GetPublicationsBibliographyModels(IEnumerable<Publication> publications)
        {
            return publications
                .Select(x => new DepartmentReportPublicationModel
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

        private async Task<List<(string, IList<DepartmentReportThemeOfScientificWorkModel>)>> GetThemeOfScientificWorksAsync(FacultyReport dbReport)
        {
            var themes = await _themeOfScientificWorkService.GetActiveForDepartmentReportAsync(dbReport.User.Cathedra.FacultyId.Value, dbReport.Date, Departments.Faculty);
            var results = new List<(string, IList<DepartmentReportThemeOfScientificWorkModel>)>();
            foreach (var financialThemes in themes)
            {
                var item = (financialThemes.Key.GetDisplayName(), new List<DepartmentReportThemeOfScientificWorkModel>());

                foreach (var theme in financialThemes.Value)
                {
                    var model = new DepartmentReportThemeOfScientificWorkModel
                    {
                        Code = theme.Code,
                        Value = theme.Value,
                        SupervisorDescription = theme.GetSupervisors(),
                        ThemeNumber = theme.ThemeNumber,
                        PeriodFrom = theme.PeriodFrom.ToString(Dates.UaDatePattern),
                        PeriodTo = theme.PeriodTo.ToString(Dates.UaDatePattern),
                        Resume = theme.Resume,
                        DefendedDissertation = theme.DefendedDissertation,
                        Publications = theme.Publications,
                        FinancialAmount = theme.FinancialAmount,
                        FinancialYear = theme.FinancialYear
                    };

                    item.Item2.Add(model);
                }

                results.Add(item);
            }

            return results;
        }

        private List<string> GetGrants(FacultyReport dbReport)
        {
            var results = new List<string>();
            foreach (var grant in dbReport.Grants)
            {
                results.Add(_themeBibliographyService.Get(grant));
            }

            return results;
        }
    }
}
