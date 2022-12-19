using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class FacultySpecification : BaseFilterSpecification<Faculty>
    {
        public FacultySpecification(BaseFilterModel filterModel)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  x => string.IsNullOrEmpty(filterModel.Search)
                        || x.Name.Contains(filterModel.Search),
                  true)
        {
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((ValueOrderType?)orderBy)
            {
                case ValueOrderType.Value when !desc: ApplyOrderBy(x => x.Name); break;
                case ValueOrderType.Value when desc: ApplyOrderByDescending(x => x.Name); break;
                default: ApplyOrderBy(x => x.Name); break;
            }
        }
    }
}
