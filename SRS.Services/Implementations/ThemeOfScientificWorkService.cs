using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
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
            var themes = await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, x => x.PeriodFrom.Year <= currentYear && x.PeriodTo.Year >= currentYear && financials.Contains(x.Financial)));
            return _mapper.Map<IList<BaseThemeOfScientificWorkModel>>(themes);
        }

        public async Task<Dictionary<Financial, IList<CathedraReportThemeOfScientificWorkModel>>> GetActiveForCathedraReportAsync(int cathedraId, DateTime date)
        {
            var themes = await _repo.GetAsync(x => x.ThemeOfScientificWorkCathedras.Any(y => y.CathedraId == cathedraId) &&
                                                    x.PeriodFrom <= DateTime.Now && x.PeriodTo >= DateTime.Now &&
                                                    x.Reports.Any(y => y.Report.Date.Value.Year == date.Year) &&
                                                    x.Financial != Financial.InternationalGrant &&
                                                    x.IsActive);

            var groupped = themes.GroupBy(x => x.Financial);
            var result = new Dictionary<Financial, IList<CathedraReportThemeOfScientificWorkModel>>();
            foreach (var group in groupped)
            {
                result.Add(group.Key, new List<CathedraReportThemeOfScientificWorkModel>());
                foreach (var theme in group)
                {
                    var mapped = _mapper.Map<CathedraReportThemeOfScientificWorkModel>(theme);

                    var report = theme.Reports
                            .OrderByDescending(x => x.Id)
                            .FirstOrDefault(r => r.Report.Date.HasValue &&
                                r.Report.Date.Value.Year == date.Year &&
                                r.Report.UserId == theme.SupervisorId);

                    var financial = theme.ThemeOfScientificWorkFinancials.FirstOrDefault(x => x.Year == date.Year);

                    mapped.Resume = report?.Resume;
                    mapped.DefendedDissertation = report?.DefendedDissertation;
                    mapped.Publications = report?.Publications;
                    mapped.FinancialAmount = financial?.Amount;
                    mapped.FinancialYear = financial?.Year;

                    result[group.Key].Add(mapped);
                }
            }

            return result;
        }

        public async Task<IList<BaseThemeOfScientificWorkModel>> GetGrantsForCathedraReportAsync(int cathedraId, DateTime date)
        {
            var themes = await _repo.GetAsync(x => x.PeriodFrom <= DateTime.Now && x.PeriodTo >= DateTime.Now &&
                                                    x.Reports.Any(y => y.Report.Date.Value.Year == date.Year && y.Report.User.CathedraId == cathedraId) &&
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
