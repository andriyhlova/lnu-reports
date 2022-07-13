using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications.Ordering
{
    public class OrdererAsc<TEntity, TOrderBy> : IOrderer<TEntity>
    {
        private readonly Expression<Func<TEntity, TOrderBy>> _expression;

        public OrdererAsc(Expression<Func<TEntity, TOrderBy>> expression)
        {
            _expression = expression;
        }

        public virtual IQueryable<TEntity> GetOrdereredQueryable(IQueryable<TEntity> query)
        {
            return query.OrderBy(_expression);
        }
    }
}
