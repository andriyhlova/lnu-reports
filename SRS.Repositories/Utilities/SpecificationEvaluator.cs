using System.Data.Entity;
using System.Linq;
using SRS.Domain.Specifications;

namespace SRS.Repositories.Utilities
{
    public static class SpecificationEvaluator<T>
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.AsNoTracking)
            {
                query = (IQueryable<T>)query.AsNoTracking();
            }

            if (specification.Includes.Any())
            {
                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (specification.Includes.Any())
            {
                query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));
            }

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.OrderBy != null)
            {
                query = specification.OrderBy.GetOrdereredQueryable(query);
            }
            else if (specification.OrderByDescending != null)
            {
                query = specification.OrderByDescending.GetOrdereredQueryable(query);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            return query;
        }
    }
}
