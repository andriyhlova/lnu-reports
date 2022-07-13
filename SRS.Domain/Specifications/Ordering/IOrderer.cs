using System;
using System.Linq;
using System.Linq.Expressions;

namespace SRS.Domain.Specifications.Ordering
{
    public interface IOrderer<TEntity>
    {
        IQueryable<TEntity> GetOrdereredQueryable(IQueryable<TEntity> query);
    }
}
