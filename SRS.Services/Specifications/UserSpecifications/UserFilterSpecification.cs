using System;
using System.Linq;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications.UserSpecifications
{
    public class UserFilterSpecification : BaseSpecification<ApplicationUser>
    {
        public UserFilterSpecification(UserFilterModel filterModel, Expression<Func<ApplicationUser, bool>> expression)
            : base(
                  expression.AndAlso(
                      x => (string.IsNullOrEmpty(filterModel.Search) ||
                                x.I18nUserInitials.Any(n => n.LastName.Contains(filterModel.Search)) ||
                                x.Email.Contains(filterModel.Search)) &&
                            (filterModel.IsActive == null || x.IsActive == filterModel.IsActive) &&
                            (filterModel.CathedraId == null || x.CathedraId == filterModel.CathedraId) &&
                            (filterModel.FacultyId == null || x.Cathedra.FacultyId == filterModel.FacultyId) &&
                            (filterModel.RoleId == null || x.Roles.Any(r => r.RoleId == filterModel.RoleId))),
                  true)
        {
            AddIncludes(x => x.Cathedra, x => x.Roles, x => x.I18nUserInitials);
            AddOrder(filterModel.OrderBy, filterModel.Desc);

            if (filterModel.Skip.HasValue && filterModel.Take.HasValue)
            {
                ApplyPaging(filterModel.Skip.Value, filterModel.Take.Value);
            }
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((UserOrderType?)orderBy)
            {
                case UserOrderType.Email when !desc: ApplyOrderBy(x => x.Email); break;
                case UserOrderType.Email when desc: ApplyOrderByDescending(x => x.Email); break;
                case UserOrderType.FirstName when !desc: ApplyOrderBy(x => x.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).FirstName); break;
                case UserOrderType.FirstName when desc: ApplyOrderByDescending(x => x.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).FirstName); break;
                case UserOrderType.LastName when !desc: ApplyOrderBy(x => x.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).LastName); break;
                case UserOrderType.LastName when desc: ApplyOrderByDescending(x => x.I18nUserInitials.FirstOrDefault(y => y.Language == Language.UA).LastName); break;
                case UserOrderType.Active when !desc: ApplyOrderBy(x => x.IsActive); break;
                case UserOrderType.Active when desc: ApplyOrderByDescending(x => x.IsActive); break;
                case UserOrderType.Roles when !desc: ApplyOrderBy(x => x.Roles.OrderBy(r => r.RoleId).Select(r => r.RoleId).FirstOrDefault()); break;
                case UserOrderType.Roles when desc: ApplyOrderByDescending(x => x.Roles.OrderBy(r => r.RoleId).Select(r => r.RoleId).FirstOrDefault()); break;
                default: ApplyOrderBy(x => x.IsActive); break;
            }
        }
    }
}
