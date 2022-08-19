using System;
using System.Linq;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class CathedraReportSpecification : BaseFilterSpecification<CathedraReport>
    {
        public CathedraReportSpecification(CathedraReportFilterModel filterModel, Expression<Func<CathedraReport, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => (filterModel.From == null || x.Date >= filterModel.From) &&
                           (filterModel.To == null || x.Date <= filterModel.To) &&
                           (filterModel.CathedraId == null || x.User.CathedraId == filterModel.CathedraId) &&
                           (filterModel.FacultyId == null || x.User.Cathedra.FacultyId == filterModel.FacultyId)),
                  true)
        {
            AddIncludes(x => x.User.Cathedra, x => x.User.I18nUserInitials);
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((CathedraReportOrderType?)orderBy)
            {
                case CathedraReportOrderType.Protocol when !desc: ApplyOrderBy(x => x.Protocol); break;
                case CathedraReportOrderType.Protocol when desc: ApplyOrderByDescending(x => x.Protocol); break;
                case CathedraReportOrderType.Date when !desc: ApplyOrderBy(x => x.Date); break;
                case CathedraReportOrderType.Date when desc: ApplyOrderByDescending(x => x.Date); break;
                case CathedraReportOrderType.Cathedra when !desc: ApplyOrderBy(x => x.User.Cathedra.Name); break;
                case CathedraReportOrderType.Cathedra when desc: ApplyOrderByDescending(x => x.User.Cathedra.Name); break;
                case CathedraReportOrderType.User when !desc: ApplyOrderBy(x => x.User.I18nUserInitials.FirstOrDefault(u => u.Language == Language.UA).LastName); break;
                case CathedraReportOrderType.User when desc: ApplyOrderByDescending(x => x.User.I18nUserInitials.FirstOrDefault(u => u.Language == Language.UA).LastName); break;
                case CathedraReportOrderType.State when !desc: ApplyOrderBy(x => x.State); break;
                case CathedraReportOrderType.State when desc: ApplyOrderByDescending(x => x.State); break;
                default: ApplyOrderByDescending(x => x.Date); break;
            }
        }
    }
}
