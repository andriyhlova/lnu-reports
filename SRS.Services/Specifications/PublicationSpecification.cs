using System;
using System.Linq;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class PublicationSpecification : BaseFilterSpecification<Publication>
    {
        public PublicationSpecification(PublicationFilterModel filterModel, Expression<Func<Publication, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => (string.IsNullOrEmpty(filterModel.Search) ||
                                x.Name.Contains(filterModel.Search)) &&
                            (filterModel.From == null || x.Date >= filterModel.From) &&
                            (filterModel.To == null || x.Date <= filterModel.To) &&
                            (filterModel.UserId == null || x.User.Any(u => u.Id == filterModel.UserId)) &&
                            (filterModel.CathedraId == null || x.User.Any(u => u.CathedraId == filterModel.CathedraId)) &&
                            (filterModel.FacultyId == null || x.User.Any(u => u.Cathedra.FacultyId == filterModel.FacultyId))),
                  true)
        {
            AddInclude(x => x.User.Select(u => u.Cathedra));
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((PublicationOrderType?)orderBy)
            {
                case PublicationOrderType.Name when !desc: ApplyOrderBy(x => x.Name); break;
                case PublicationOrderType.Name when desc: ApplyOrderByDescending(x => x.Name); break;
                case PublicationOrderType.Date when !desc: ApplyOrderBy(x => x.Date); break;
                case PublicationOrderType.Date when desc: ApplyOrderByDescending(x => x.Date); break;
                case PublicationOrderType.PublicationType when !desc: ApplyOrderBy(x => x.PublicationType); break;
                case PublicationOrderType.PublicationType when desc: ApplyOrderByDescending(x => x.PublicationType); break;
                case PublicationOrderType.AuthorsOrder when !desc: ApplyOrderBy(x => x.AuthorsOrder); break;
                case PublicationOrderType.AuthorsOrder when desc: ApplyOrderByDescending(x => x.AuthorsOrder); break;
                default: ApplyOrderByDescending(x => x.Date); break;
            }
        }
    }
}
