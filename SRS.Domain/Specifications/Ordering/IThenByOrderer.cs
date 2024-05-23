using System.Linq;

namespace SRS.Domain.Specifications.Ordering
{
    public interface IThenByOrderer<TEntity>
    {
        IOrderedQueryable<TEntity> GetOrdereredQueryable(IOrderedQueryable<TEntity> query);
    }
}
