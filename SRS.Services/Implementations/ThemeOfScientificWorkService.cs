using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Specifications;
using SRS.Repositories.Interfaces;
using SRS.Services.Interfaces;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.ThemeOfScientificWorkModels;

namespace SRS.Services.Implementations
{
    public class ThemeOfScientificWorkService : BaseService<ThemeOfScientificWork>, IThemeOfScientificWorkService
    {
        public ThemeOfScientificWorkService(IBaseRepository<ThemeOfScientificWork> repo, IMapper mapper)
            : base(repo, mapper)
        {
        }

        public async Task<IList<BaseThemeOfScientificWorkModel>> GetAsync(ThemeOfScientificWorkFilterModel filterModel)
        {
            var scientificThemes = await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, null));
            return _mapper.Map<IList<BaseThemeOfScientificWorkModel>>(scientificThemes);
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
                PeriodToTo = filterModel.PeriodToTo
            };

            return await _repo.CountAsync(new ThemeOfScientificWorkSpecification(countFilterModel, null));
        }

        public async Task<IList<BaseThemeOfScientificWorkModel>> GetActiveAsync(ThemeOfScientificWorkFilterModel filterModel, params Financial[] financials)
        {
            var currentYear = new DateTime(DateTime.Now.Year, 1, 1);
            var themes = await _repo.GetAsync(new ThemeOfScientificWorkSpecification(filterModel, x => x.PeriodFrom <= currentYear && x.PeriodTo >= currentYear && financials.Contains(x.Financial)));
            return _mapper.Map<IList<BaseThemeOfScientificWorkModel>>(themes);
        }

        public async Task<IList<ThemeOfScientificWorkModel>> GetActiveForCathedraReportAsync(int cathedraId, Financial financial)
        {
            var themes = await _repo.GetAsync(x => x.Financial == financial
                                                   && x.Reports.Any(y => y.User.CathedraId == cathedraId && y.State == ReportState.Confirmed));
            return _mapper.Map<IList<ThemeOfScientificWorkModel>>(themes ?? new List<ThemeOfScientificWork>());
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetAsync(id);
            entity.IsActive = false;
            await _repo.UpdateAsync(entity);
            return true;
        }
    }
}
