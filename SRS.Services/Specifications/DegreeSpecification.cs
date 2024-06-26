﻿using SRS.Domain.Entities;
using SRS.Services.Models.FilterModels;

namespace SRS.Domain.Specifications
{
    public class DegreeSpecification : BaseFilterSpecification<Degree>
    {
        public DegreeSpecification(BaseFilterModel filterModel)
            : base(
                  filterModel.Skip,
                  filterModel.Take,
                  x => string.IsNullOrEmpty(filterModel.Search)
                        || x.Value.Contains(filterModel.Search),
                  true)
        {
            if (filterModel.Desc)
            {
                ApplyOrderByDescending(x => x.Value);
            }
            else
            {
                ApplyOrderBy(x => x.Value);
            }
        }
    }
}
