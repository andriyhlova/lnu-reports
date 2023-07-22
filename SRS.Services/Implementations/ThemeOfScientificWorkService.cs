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

        public async Task<IList<ThemeOfScientificWorkModel>> GetActiveForCathedraReportAsync(int cathedraId, Financial financial)
        {
            var themes = await _repo.GetAsync(x => x.Financial == financial
                                                   && x.Reports.Any(y => y.ThemeOfScientificWork.User.CathedraId == cathedraId && y.Report.State == ReportState.Confirmed));
            return _mapper.Map<IList<ThemeOfScientificWorkModel>>(themes ?? new List<ThemeOfScientificWork>());
        }

        public async Task<IList<CathedraReportThemeOfScientificWorkModel>> GetActiveForCathedraReport1Async(int cathedraId, Financial financial)
        {
            var themes = await _repo.GetAsync(x => x.Financial == financial &&
                                                    x.ThemeOfScientificWorkCathedras.Any(y => y.CathedraId == cathedraId) &&
                                                    x.PeriodFrom <= DateTime.Now && x.PeriodTo >= DateTime.Now &&
                                                    x.IsActive);

            return _mapper.Map<IList<CathedraReportThemeOfScientificWorkModel>>(themes);
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
