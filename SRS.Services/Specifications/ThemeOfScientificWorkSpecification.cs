using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications
{
    public class ThemeOfScientificWorkSpecification : BaseFilterSpecification<ThemeOfScientificWork>
    {
        public ThemeOfScientificWorkSpecification(ThemeOfScientificWorkFilterModel filterModel, Expression<Func<ThemeOfScientificWork, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => (filterModel.IsActive == null || x.IsActive == filterModel.IsActive) &&
                                (filterModel.Financial == null || x.Financial == filterModel.Financial) &&
                                (filterModel.SubCategory == null || x.SubCategory == filterModel.SubCategory) &&
                                (filterModel.PeriodFromFrom == null || x.PeriodFrom >= filterModel.PeriodFromFrom) &&
                                (filterModel.PeriodFromTo == null || x.PeriodFrom <= filterModel.PeriodFromTo) &&
                                (filterModel.PeriodToFrom == null || x.PeriodTo >= filterModel.PeriodToFrom) &&
                                (filterModel.PeriodToTo == null || x.PeriodTo <= filterModel.PeriodToTo) &&
                                (filterModel.SupervisorId == null || x.ThemeOfScientificWorkSupervisors.Any(y => y.SupervisorId == filterModel.SupervisorId)) &&
                                (string.IsNullOrEmpty(filterModel.Search) ||
                                x.ThemeNumber.Contains(filterModel.Search) ||
                                x.OtherProjectType.Contains(filterModel.Search) ||
                                x.Code.Contains(filterModel.Search) ||
                                x.Value.Contains(filterModel.Search) ||
                                x.ThemeOfScientificWorkSupervisors.Any(z => z.Supervisor.I18nUserInitials.Any(y => y.FirstName.Contains(filterModel.Search) ||
                                                                       y.LastName.Contains(filterModel.Search) ||
                                                                       y.FathersName.Contains(filterModel.Search))))),
                  true)
        {
            AddIncludes(x => x.ThemeOfScientificWorkSupervisors.Select(y => y.Supervisor.I18nUserInitials));
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
                case ThemeOfScientificWorkOrderType.SubCategory when !desc: ApplyOrderBy(x => x.SubCategory); break;
                case ThemeOfScientificWorkOrderType.SubCategory when desc: ApplyOrderByDescending(x => x.SubCategory); break;
                case ThemeOfScientificWorkOrderType.FinancialAmount when !desc: ApplyOrderBy(x => x.ThemeOfScientificWorkFinancials.Sum(y => y.Amount)); break;
                case ThemeOfScientificWorkOrderType.FinancialAmount when desc: ApplyOrderByDescending(x => x.ThemeOfScientificWorkFinancials.Sum(y => y.Amount)); break;
                case ThemeOfScientificWorkOrderType.PlannedAmount when !desc: ApplyOrderBy(x => x.PlannedAmount); break;
                case ThemeOfScientificWorkOrderType.PlannedAmount when desc: ApplyOrderByDescending(x => x.PlannedAmount); break;
                case ThemeOfScientificWorkOrderType.Currency when !desc: ApplyOrderBy(x => x.Currency); break;
                case ThemeOfScientificWorkOrderType.Currency when desc: ApplyOrderByDescending(x => x.Currency); break;
                case ThemeOfScientificWorkOrderType.Cathedras when desc: ApplyOrderBy(x => x.ThemeOfScientificWorkCathedras.OrderBy(y => y.Cathedra.Name).FirstOrDefault().Cathedra.Name); break;
                case ThemeOfScientificWorkOrderType.Cathedras when desc: ApplyOrderByDescending(x => x.ThemeOfScientificWorkCathedras.OrderBy(y => y.Cathedra.Name).FirstOrDefault().Cathedra.Name); break;
                case ThemeOfScientificWorkOrderType.Faculties when desc: ApplyOrderBy(x => x.ThemeOfScientificWorkCathedras.OrderBy(y => y.Cathedra.Faculty.Name).FirstOrDefault().Cathedra.Faculty.Name); break;
                case ThemeOfScientificWorkOrderType.Faculties when desc: ApplyOrderByDescending(x => x.ThemeOfScientificWorkCathedras.OrderBy(y => y.Cathedra.Faculty.Name).FirstOrDefault().Cathedra.Faculty.Name); break;
                default: ApplyOrderByDescending(x => x.PeriodTo); ApplyThenBy(x => x.Value); break;
            }
        }
    }
}
