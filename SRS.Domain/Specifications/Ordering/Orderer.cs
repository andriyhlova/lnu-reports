using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications.Ordering
{
    public class Orderer<TEntity, TOrderBy> : BaseOrderer<TEntity, TOrderBy>, IOrderer<TEntity>
    {
        public Orderer(Expression<Func<TEntity, TOrderBy>> expression, bool ascending = true)
            : base(expression, ascending)
        {
        }

        public virtual IOrderedQueryable<TEntity> GetOrdereredQueryable(IQueryable<TEntity> query)
        {
            return _ascending ? query.OrderBy(_expression) : query.OrderByDescending(_expression);
        }
    }
}
