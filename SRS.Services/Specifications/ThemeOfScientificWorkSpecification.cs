using System;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class ThemeOfScientificWorkSpecification : BaseFilterSpecification<ThemeOfScientificWork>
    {
        public ThemeOfScientificWorkSpecification(DepartmentFilterModel filterModel, Expression<Func<ThemeOfScientificWork, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => string.IsNullOrEmpty(filterModel.Search) ||
                                x.ThemeNumber.Contains(filterModel.Search) ||
                                x.OtherProjectType.Contains(filterModel.Search) ||
                                x.Code.Contains(filterModel.Search) ||
                                x.Value.Contains(filterModel.Search) ||
                                x.ScientificHead.Contains(filterModel.Search)),
                  true)
        {
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((ThemeOfScientificWorkOrderType?)orderBy)
            {
                case ThemeOfScientificWorkOrderType.ThemeNumber when !desc: ApplyOrderBy(x => x.ThemeNumber); break;
                case ThemeOfScientificWorkOrderType.ThemeNumber when desc: ApplyOrderByDescending(x => x.ThemeNumber); break;
                case ThemeOfScientificWorkOrderType.Code when !desc: ApplyOrderBy(x => x.Code); break;
                case ThemeOfScientificWorkOrderType.Code when desc: ApplyOrderByDescending(x => x.Code); break;
                case ThemeOfScientificWorkOrderType.Value when !desc: ApplyOrderBy(x => x.Value); break;
                case ThemeOfScientificWorkOrderType.Value when desc: ApplyOrderByDescending(x => x.Value); break;
                case ThemeOfScientificWorkOrderType.ScientificHead when !desc: ApplyOrderBy(x => x.ScientificHead); break;
                case ThemeOfScientificWorkOrderType.ScientificHead when desc: ApplyOrderByDescending(x => x.ScientificHead); break;
                case ThemeOfScientificWorkOrderType.PeriodTo when !desc: ApplyOrderBy(x => x.PeriodTo); break;
                case ThemeOfScientificWorkOrderType.PeriodTo when desc: ApplyOrderByDescending(x => x.PeriodTo); break;
                case ThemeOfScientificWorkOrderType.Financial when !desc: ApplyOrderBy(x => x.Financial); break;
                case ThemeOfScientificWorkOrderType.Financial when desc: ApplyOrderByDescending(x => x.Financial); break;
                case ThemeOfScientificWorkOrderType.CathedraName when !desc: ApplyOrderBy(x => x.Cathedra.Name); break;
                case ThemeOfScientificWorkOrderType.CathedraName when desc: ApplyOrderByDescending(x => x.Cathedra.Name); break;
                default: ApplyOrderByDescending(x => x.PeriodTo); break;
            }
        }
    }
}
