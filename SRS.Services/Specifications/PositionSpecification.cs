using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class PositionSpecification : BaseFilterSpecification<Position>
    {
        public PositionSpecification(BaseFilterModel filterModel)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  x => string.IsNullOrEmpty(filterModel.Search)
                        || x.Value.Contains(filterModel.Search),
                  true)
        {
            AddOrder(filterModel.OrderBy, filterModel.Desc);
        }

        private void AddOrder(int? orderBy, bool desc)
        {
            switch ((PositionOrderType?)orderBy)
            {
                case PositionOrderType.Value when !desc: ApplyOrderBy(x => x.Value); break;
                case PositionOrderType.Value when desc: ApplyOrderByDescending(x => x.Value); break;
                case PositionOrderType.SortOrder when !desc: ApplyOrderBy(x => x.SortOrder); break;
                case PositionOrderType.SortOrder when desc: ApplyOrderByDescending(x => x.SortOrder); break;
                default: ApplyOrderBy(x => x.SortOrder); break;
            }
        }
    }
}
