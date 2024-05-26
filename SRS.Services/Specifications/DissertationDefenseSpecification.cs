using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Enums.OrderTypes;
using SRS.Domain.Specifications;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Services.Specifications
{
    public class DissertationDefenseSpecification : BaseFilterSpecification<DissertationDefense>
    {
        public DissertationDefenseSpecification(DissertationDefenseFilterModel filterModel, Expression<Func<DissertationDefense, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => (filterModel.PeriodFrom == null || x.DefenseDate >= filterModel.PeriodFrom) &&
                           (filterModel.PeriodTo == null || x.DefenseDate <= filterModel.PeriodTo) &&
                           (filterModel.YearOfGraduatingFrom == null || x.YearOfGraduating >= filterModel.YearOfGraduatingFrom) &&
                           (filterModel.YearOfGraduatingTo == null || x.YearOfGraduating <= filterModel.YearOfGraduatingTo) &&
                           (filterModel.FacultyId == null || x.User.Cathedra.FacultyId == filterModel.FacultyId) &&
                           (filterModel.CathedraId == null || x.User.CathedraId == filterModel.CathedraId) &&
                           (filterModel.SupervisorId == null || x.Supervisor.Id == filterModel.SupervisorId) &&
                           (string.IsNullOrEmpty(filterModel.Search) ||
                           x.Theme.Contains(filterModel.Search) ||
                           x.Supervisor.I18nUserInitials.Any(y => y.FirstName.Contains(filterModel.Search) ||
                                                                    y.LastName.Contains(filterModel.Search) ||
                                                                    y.FathersName.Contains(filterModel.Search)))),
                  true)
        {
            AddIncludes(x => x.Supervisor.I18nUserInitials);
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((DissertationDefenseOrderType?)orderBy)
            {
                case DissertationDefenseOrderType.Theme when !desc: ApplyOrderBy(x => x.Theme); break;
                case DissertationDefenseOrderType.Theme when desc: ApplyOrderByDescending(x => x.Theme); break;
                case DissertationDefenseOrderType.DefenseDate when !desc: ApplyOrderBy(x => x.DefenseDate); break;
                case DissertationDefenseOrderType.DefenseDate when desc: ApplyOrderByDescending(x => x.DefenseDate); break;
                case DissertationDefenseOrderType.YearOfGraduating when !desc: ApplyOrderBy(x => x.YearOfGraduating); break;
                case DissertationDefenseOrderType.YearOfGraduating when desc: ApplyOrderByDescending(x => x.YearOfGraduating); break;
                case DissertationDefenseOrderType.SubmissionDate when !desc: ApplyOrderBy(x => x.SubmissionDate); break;
                case DissertationDefenseOrderType.SubmissionDate when desc: ApplyOrderByDescending(x => x.SubmissionDate); break;
                case DissertationDefenseOrderType.SupervisorId when !desc: ApplyOrderBy(x => x.Supervisor.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).LastName); ApplyThenBy(x => x.Supervisor.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).FirstName); break;
                case DissertationDefenseOrderType.SupervisorId when desc: ApplyOrderByDescending(x => x.Supervisor.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).LastName); ApplyThenByDescending(x => x.Supervisor.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).FirstName); break;
                case DissertationDefenseOrderType.UserId when !desc: ApplyOrderBy(x => x.User.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).LastName); ApplyThenBy(x => x.Supervisor.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).FirstName); break;
                case DissertationDefenseOrderType.UserId when desc: ApplyOrderByDescending(x => x.User.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).LastName); ApplyThenByDescending(x => x.Supervisor.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).FirstName); break;
                case DissertationDefenseOrderType.DissertationType when !desc: ApplyOrderBy(x => x.DissertationType); break;
                case DissertationDefenseOrderType.DissertationType when desc: ApplyOrderByDescending(x => x.DissertationType); break;
                default: ApplyOrderByDescending(x => x.DefenseDate); ApplyThenBy(x => x.DefenseDate); break;
            }
        }
    }
}
