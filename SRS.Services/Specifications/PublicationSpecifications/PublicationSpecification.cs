using System;
using System.Linq;
using System.Linq.Expressions;
using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Extensions;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications.PublicationSpecifications
{
    public class PublicationSpecification : BaseFilterSpecification<Publication>
    {
        public PublicationSpecification(PublicationFilterModel filterModel, Expression<Func<Publication, bool>> expression)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  expression.AndAlso(
                      x => (string.IsNullOrEmpty(filterModel.Search) ||
                            x.Name.Contains(filterModel.Search) ||
                            x.AuthorsOrder.Contains(filterModel.Search) ||
                            x.Place.Contains(filterModel.Search) ||
                            x.Tome.Contains(filterModel.Search) ||
                            x.Edition.Contains(filterModel.Search) ||
                            x.Journal.Name.Contains(filterModel.Search) ||
                            x.OtherJournal.Contains(filterModel.Search) ||
                            x.DOI.Contains(filterModel.Search)) &&
                            (filterModel.PublicationType == null || x.PublicationType == filterModel.PublicationType) &&
                            (filterModel.From == null || x.Date >= filterModel.From) &&
                            (filterModel.To == null || x.Date <= filterModel.To) &&
                            (filterModel.UserId == null || x.User.Any(u => u.Id == filterModel.UserId)) &&
                            (filterModel.CathedraId == null || x.User.Any(u => u.CathedraId == filterModel.CathedraId)) &&
                            (filterModel.FacultyId == null || x.User.Any(u => u.Cathedra.FacultyId == filterModel.FacultyId))),
                  true)
        {
            AddInclude(x => x.User.Select(u => u.Cathedra));
            AddInclude(x => x.Journal);
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
                case PublicationOrderType.Journal when !desc: ApplyOrderBy(x => x.Journal.Name ?? x.OtherJournal); break;
                case PublicationOrderType.Journal when desc: ApplyOrderByDescending(x => x.Journal.Name ?? x.OtherJournal); break;
                default: ApplyOrderByDescending(x => x.Date); break;
            }
        }
    }
}
