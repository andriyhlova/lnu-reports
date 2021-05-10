using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Specifications;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ScientificReport.DAL.Implementation
{
    public class BaseRepository<TEntity,T>: IGenericRepository<TEntity,T> where TEntity:class,IBaseEntity<T>
    {
        private readonly IDbContext context;
        private readonly DbSet<TEntity> dbSet;
        public BaseRepository(IDbContext _context)
        {
            context = _context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<T> CreateAsync(TEntity item)
        {
            dbSet.Add(item);
            await context.SaveChangesAsync();
            return item.Id;
        }
        public async Task RemoveAsync(TEntity item)
        {
            dbSet.Attach(item);
            dbSet.Remove(item);
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity item)
        {
            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
        public async Task<TEntity> FindByIdAsync(T id)
        {
            return await dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAsync<TOrder>(QuerySpecification<TEntity, TOrder> specification)
        {
            var query = dbSet.AsNoTracking().AsQueryable();
            if(specification.PredicateExpression != null)
            {
                query = query.Where(specification.PredicateExpression);
            }

            if(specification.OrderExpression != null)
            {
                if (specification.Asc)
                {
                    query = query.OrderBy(specification.OrderExpression);
                }
                else
                {
                    query = query.OrderByDescending(specification.OrderExpression);
                }
            }

            if(specification.Take > 0)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return await query.ToListAsync();
        }
    }
}
