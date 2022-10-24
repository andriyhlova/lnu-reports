using System;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications.Ordering
{
    public class BaseOrderer<TEntity, TOrderBy>
    {
        protected readonly Expression<Func<TEntity, TOrderBy>> _expression;
        protected readonly bool _ascending;

        public BaseOrderer(Expression<Func<TEntity, TOrderBy>> expression, bool ascending = true)
        {
            _expression = expression;
            _ascending = ascending;
        }
    }
}
