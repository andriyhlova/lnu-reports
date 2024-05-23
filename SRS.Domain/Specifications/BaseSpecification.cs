using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SRS.Domain.Specifications.Ordering;

namespace SRS.Domain.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria = null, bool asNoTracking = false)
        {
            Criteria = criteria;
            AsNoTracking = asNoTracking;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public bool AsNoTracking { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        public IOrderer<T> OrderByOrderer { get; private set; }

        public ICollection<IThenByOrderer<T>> ThenByOrderers { get; private set; } = new List<IThenByOrderer<T>>();

        public Expression<Func<T, object>> GroupBy { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        protected virtual void AddIncludes(params Expression<Func<T, object>>[] includeExpressions)
        {
            Includes.AddRange(includeExpressions);
        }

        protected virtual void AddIncludes(params string[] includeStrings)
        {
            IncludeStrings.AddRange(includeStrings);
        }

        protected virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyOrderBy<TOrderBy>(Expression<Func<T, TOrderBy>> orderByExpression)
        {
            OrderByOrderer = new Orderer<T, TOrderBy>(orderByExpression);
        }

        protected virtual void ApplyOrderByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderByDescendingExpression)
        {
            OrderByOrderer = new Orderer<T, TOrderBy>(orderByDescendingExpression, false);
        }

        protected virtual void ApplyThenBy<TOrderBy>(Expression<Func<T, TOrderBy>> thenByExpression)
        {
            if (ThenByOrderers == null)
            {
                ThenByOrderers = new List<IThenByOrderer<T>>();
            }

            ThenByOrderers.Add(new ThenByOrderer<T, TOrderBy>(thenByExpression));
        }

        protected virtual void ApplyThenByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> thenByDescendingExpression)
        {
            if (ThenByOrderers == null)
            {
                ThenByOrderers = new List<IThenByOrderer<T>>();
            }

            ThenByOrderers.Add(new ThenByOrderer<T, TOrderBy>(thenByDescendingExpression, false));
        }

        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
