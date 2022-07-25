using System;
using System.Linq;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class ReportSpecification : BaseFilterSpecification<Report>
    {
        public ReportSpecification(ReportFilterModel filterModel, Expression<Func<Report, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => (filterModel.From == null || x.Date >= filterModel.From) &&
                           (filterModel.To == null || x.Date <= filterModel.To) &&
                           (filterModel.UserId == null || x.User.Id == filterModel.UserId) &&
                           (filterModel.CathedraId == null || x.User.CathedraId == filterModel.CathedraId) &&
                           (filterModel.FacultyId == null || x.User.Cathedra.FacultyId == filterModel.FacultyId)),
                  true)
        {
            AddIncludes(x => x.User.Cathedra, x => x.User.I18nUserInitials);
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((ReportOrderType?)orderBy)
            {
                case ReportOrderType.Protocol when !desc: ApplyOrderBy(x => x.Protocol); break;
                case ReportOrderType.Protocol when desc: ApplyOrderByDescending(x => x.Protocol); break;
                case ReportOrderType.Date when !desc: ApplyOrderBy(x => x.Date); break;
                case ReportOrderType.Date when desc: ApplyOrderByDescending(x => x.Date); break;
                case ReportOrderType.IsSigned when !desc: ApplyOrderBy(x => x.IsSigned); break;
                case ReportOrderType.IsSigned when desc: ApplyOrderByDescending(x => x.IsSigned); break;
                case ReportOrderType.IsConfirmed when !desc: ApplyOrderBy(x => x.IsConfirmed); break;
                case ReportOrderType.IsConfirmed when desc: ApplyOrderByDescending(x => x.IsConfirmed); break;
                case ReportOrderType.User when !desc: ApplyOrderBy(x => x.User.I18nUserInitials.FirstOrDefault(u => u.Language == Language.UA).LastName); break;
                case ReportOrderType.User when desc: ApplyOrderByDescending(x => x.User.I18nUserInitials.FirstOrDefault(u => u.Language == Language.UA).LastName); break;
                default: ApplyOrderByDescending(x => x.Date); break;
            }
        }
    }
}
