using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Domain.Specifications;
using SRS.Services.Models.FilterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Specifications
{
    public class SpecializationSpecification : BaseFilterSpecification<Specialization>
    {
        public SpecializationSpecification(BaseFilterModel filterModel)
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
                case ValueOrderType.SortOrder when !desc: ApplyOrderBy(x => x.Code); break;
                case ValueOrderType.SortOrder when desc: ApplyOrderByDescending(x => x.Code); break;
                default: ApplyOrderBy(x => x.Code); break;
            }
        }
    }
}
