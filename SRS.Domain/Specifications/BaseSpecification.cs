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

        public IOrderer<T> OrderBy { get; private set; }

        public IOrderer<T> OrderByDescending { get; private set; }

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
            OrderBy = new OrdererAsc<T, TOrderBy>(orderByExpression);
            OrderByDescending = null;
        }

        protected virtual void ApplyOrderByDescending<TOrderBy>(Expression<Func<T, TOrderBy>> orderByDescendingExpression)
        {
            OrderBy = null;
            OrderByDescending = new OrdererDesc<T, TOrderBy>(orderByDescendingExpression);
        }

        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}
