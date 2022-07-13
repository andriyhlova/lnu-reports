using System;
using System.Linq.Expressions;
using SRS.Domain.Entities;

namespace SRS.Domain.Specifications
{
    public class BaseFilterSpecification<T> : BaseSpecification<T>
        where T : BaseEntity
    {
        public BaseFilterSpecification(int? skip, int? take, Expression<Func<T, bool>> criteria = null, bool asNoTracking = false)
            : base(criteria, asNoTracking)
        {
            ApplyOrderBy(x => x.Id);
            AddPagination(skip, take);
        }

        private void AddPagination(int? skip, int? take)
        {
            if (skip.HasValue && take.HasValue)
            {
                ApplyPaging(skip.Value, take.Value);
            }
        }
    }
}
