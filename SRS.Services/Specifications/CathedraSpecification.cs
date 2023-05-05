using System.Linq;
using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class CathedraSpecification : BaseFilterSpecification<Cathedra>
    {
        public CathedraSpecification(FacultyFilterModel filterModel)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  x => (string.IsNullOrEmpty(filterModel.Search)
                                || x.Name.Contains(filterModel.Search)
                                || x.Faculty.Name.Contains(filterModel.Search))
                                && (filterModel.FacultyId == null
                                || x.FacultyId == filterModel.FacultyId),
                  true)
        {
            AddInclude(x => x.Faculty);
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((CathedraOrderType?)orderBy)
            {
                case CathedraOrderType.Name when !desc: ApplyOrderBy(x => x.Name); break;
                case CathedraOrderType.Name when desc: ApplyOrderByDescending(x => x.Name); break;
                case CathedraOrderType.Faculty when !desc: ApplyOrderBy(x => x.Faculty.Name); break;
                case CathedraOrderType.Faculty when desc: ApplyOrderByDescending(x => x.Faculty.Name); break;
                default: ApplyOrderBy(x => x.Name); break;
            }
        }
    }
}
