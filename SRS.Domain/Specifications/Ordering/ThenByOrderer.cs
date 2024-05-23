using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications.Ordering
{
    public class ThenByOrderer<TEntity, TOrderBy> : BaseOrderer<TEntity, TOrderBy>, IThenByOrderer<TEntity>
    {
        public ThenByOrderer(Expression<Func<TEntity, TOrderBy>> expression, bool ascending = true)
            : base(expression, ascending)
        {
        }

        public virtual IOrderedQueryable<TEntity> GetOrdereredQueryable(IOrderedQueryable<TEntity> query)
        {
            return _ascending ? query.ThenBy(_expression) : query.ThenByDescending(_expression);
        }
    }
}
