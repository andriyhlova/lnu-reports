using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications.Ordering
{
    public class OrdererDesc<TEntity, TOrderBy> : IOrderer<TEntity>
    {
        private readonly Expression<Func<TEntity, TOrderBy>> _expression;

        public OrdererDesc(Expression<Func<TEntity, TOrderBy>> expression)
        {
            _expression = expression;
        }

        public virtual IQueryable<TEntity> GetOrdereredQueryable(IQueryable<TEntity> query)
        {
            return query.OrderByDescending(_expression);
        }
    }
}
