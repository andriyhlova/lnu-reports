using AutoMapper;
using DocumentFormat.OpenXml.Bibliography;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.Services.Implementations
{
    public class ThemeOfScientificWorkService : BaseService<ThemeOfScientificWork>, IThemeOfScientificWorkService
    {
        public ThemeOfScientificWorkService(IBaseRepository<ThemeOfScientificWork> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<BaseThemeOfScientificWorkWithFinancialsModel>> GetAsync(ThemeOfScientificWorkFilterModel filterModel)
        {
            var scientificThemes = await _repo.GetAsync(new ThemeOfScientificWorkWithFinancialsSpecification(filterModel, null));
            return _mapper.Map<IList<BaseThemeOfScientificWorkWithFinancialsModel>>(scientificThemes);
        }

        public async Task<int> CountAsync(ThemeOfScientificWorkFilterModel filterModel)
        {
            var countFilterModel = new ThemeOfScientificWorkFilterModel
            {
                Search = filterModel.Search,
                Financial = filterModel.Financial,
                SubCategory = filterModel.SubCategory,
                IsActive = filterModel.IsActive,
                PeriodFromFrom = filterModel.PeriodFromFrom,
                PeriodFromTo = filterModel.PeriodFromTo,
                PeriodToFrom = filterModel.PeriodToFrom,
                PeriodToTo = filterModel.PeriodToTo,
                FacultyId = filterModel.FacultyId,
                CathedraId = filterModel.CathedraId
            };

            return await _repo.CountAsync(new ThemeOfScientificWorkWithFinancialsSpecification(countFilterModel, null));
        }

        public async Task<IList<BaseThemeOfScientificWorkModel>> GetActiveAsync(ThemeOfScientificWorkFilterModel filterModel, params Financial[] financials)
        {
            var currentYear = DateTime.Now.Year;
            var previousYear = currentYear - 1;
            var themes = await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, x => x.PeriodFrom.Year <= currentYear && (x.PeriodTo.Year >= currentYear || x.PeriodTo.Year == previousYear) && financials.Contains(x.Financial)));
            return _mapper.Map<IList<BaseThemeOfScientificWorkModel>>(themes);
        }

        public async Task<Dictionary<Financial, IList<DepartmentReportThemeOfScientificWorkModel>>> GetActiveForDepartmentReportAsync(
            int departmentId,
            DateTime? date,
            string department)
        {
            var reportDate = date ?? DateTime.Now;
            var currentYear = reportDate.Year;
            var previousYear = currentYear - 1;
            var themes = new List<ThemeOfScientificWork>();

            if (department == Departments.Cathedra)
            {
                themes = await _repo.GetAsync(x => x.ThemeOfScientificWorkCathedras.Any(y => y.CathedraId == departmentId) &&
                                                    x.PeriodFrom.Year <= currentYear && (x.PeriodTo.Year >= currentYear || x.PeriodTo.Year == previousYear) &&
                                                    x.Reports.Any(y => y.Report.Date.Value.Year == reportDate.Year && y.Report.User.CathedraId == departmentId) &&
                                                    x.Financial != Financial.InternationalGrant &&
                                                    x.IsActive);
            }
            else if (department == Departments.Faculty)
            {
                themes = await _repo.GetAsync(x => x.ThemeOfScientificWorkCathedras.Any(y => y.Cathedra.FacultyId == departmentId) &&
                                                    x.PeriodFrom.Year <= currentYear && (x.PeriodTo.Year >= currentYear || x.PeriodTo.Year == previousYear) &&
                                                    x.Reports.Any(y => y.Report.Date.Value.Year == reportDate.Year && y.Report.User.Cathedra.FacultyId == departmentId) &&
                                                    x.Financial != Financial.InternationalGrant &&
                                                    x.IsActive);
            }

            var groupped = themes.GroupBy(x => x.Financial);
            var result = new Dictionary<Financial, IList<DepartmentReportThemeOfScientificWorkModel>>();
            foreach (var group in groupped)
            {
                result.Add(group.Key, new List<DepartmentReportThemeOfScientificWorkModel>());
                foreach (var theme in group)
                {
                    var mapped = _mapper.Map<DepartmentReportThemeOfScientificWorkModel>(theme);

                    var report = new ReportThemeOfScientificWork();

                    if (department == Departments.Cathedra)
                    {
                        report = theme.Reports
                            .FirstOrDefault(r => r.Report.Date.HasValue &&
                                r.Report.Date.Value.Year == reportDate.Year &&
                                r.Report.User.CathedraId == departmentId);
                    }
                    else if (department == Departments.Faculty)
                    {
                        report = theme.Reports
                            .FirstOrDefault(r => r.Report.Date.HasValue &&
                                r.Report.Date.Value.Year == reportDate.Year &&
                                r.Report.User.Cathedra.FacultyId == departmentId);
                    }

                    var financial = theme.ThemeOfScientificWorkFinancials.FirstOrDefault(x => x.Year == reportDate.Year);

                    mapped.Resume = report?.Resume;
                    mapped.DefendedDissertation = report?.DefendedDissertation;
                    mapped.Publications = report?.Publications;
                    mapped.FinancialAmount = financial?.Amount;
                    mapped.FinancialYear = financial?.Year;
                    mapped.AmountOfApplicationUserFullTime = report?.ApplicationUserFullTime.Count;
                    mapped.AmountOfApplicationUserExternalPartTime = report?.ApplicationUserExternalPartTime.Count;
                    mapped.AmountOfApplicationUserLawContract = report?.ApplicationUserLawContract.Count;

                    result[group.Key].Add(mapped);
                }
            }

            return result;
        }

        public async Task<IList<BaseThemeOfScientificWorkModel>> GetGrantsForCathedraReportAsync(int cathedraId, DateTime? date)
        {
            var reportDate = date ?? DateTime.Now;
            var currentYear = reportDate.Year;
            var previousYear = currentYear - 1;
            var themes = await _repo.GetAsync(x => x.ThemeOfScientificWorkCathedras.Any(y => y.CathedraId == cathedraId) &&
                                                    x.PeriodFrom.Year <= currentYear && (x.PeriodTo.Year >= currentYear || x.PeriodTo.Year == previousYear) &&
                                                    x.Reports.Any(y => y.Report.Date.Value.Year == reportDate.Year && y.Report.User.CathedraId == cathedraId) &&
                                                    x.Financial == Financial.InternationalGrant &&
                                                    x.IsActive);

            return _mapper.Map<IList<BaseThemeOfScientificWorkModel>>(themes.Distinct());
        }

        public async Task<bool> ToggleActivationAsync(int id)
        {
            var entity = await _repo.GetAsync(id);
            entity.IsActive = !entity.IsActive;
            await _repo.UpdateAsync(entity);
            return true;
        }
    }
}
