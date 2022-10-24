using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SRS.Domain.Specifications.Ordering;

namespace SRS.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        bool AsNoTracking { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        List<string> IncludeStrings { get; }

        IOrderer<T> OrderByOrderer { get; }

        ICollection<IThenByOrderer<T>> ThenByOrderers { get; }

        Expression<Func<T, object>> GroupBy { get; }

        int Take { get; }

        int Skip { get; }

        bool IsPagingEnabled { get; }
    }
}
