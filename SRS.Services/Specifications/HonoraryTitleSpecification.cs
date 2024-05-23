using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class HonoraryTitleSpecification : BaseFilterSpecification<HonoraryTitle>
    {
        public HonoraryTitleSpecification(BaseFilterModel filterModel)
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
            switch ((ValueOrderType?)orderBy)
            {
                case ValueOrderType.Value when !desc: ApplyOrderBy(x => x.Value); break;
                case ValueOrderType.Value when desc: ApplyOrderByDescending(x => x.Value); break;
                case ValueOrderType.SortOrder when !desc: ApplyOrderBy(x => x.SortOrder); break;
                case ValueOrderType.SortOrder when desc: ApplyOrderByDescending(x => x.SortOrder); break;
                default: ApplyOrderBy(x => x.SortOrder); break;
            }
        }
    }
}
