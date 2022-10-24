using System.Linq;

namespace SRS.Domain.Specifications.Ordering
{
    public interface IOrderer<TEntity>
    {
        IOrderedQueryable<TEntity> GetOrdereredQueryable(IQueryable<TEntity> query);
    }
}
