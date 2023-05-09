using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SRS.Domain.Entities;
using SRS.Domain.Enums.OrderTypes;
using SRS.Domain.Specifications;
using SRS.Services.Models.FilterModels;

namespace SRS.Services.Specifications
{
    public class CathedraSpecification : BaseFilterSpecification<Cathedra>
    {
        public CathedraSpecification(BaseFilterModel filterModel)
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
                default: ApplyOrderBy(x => x.Name); break;
            }
        }
    }
}
