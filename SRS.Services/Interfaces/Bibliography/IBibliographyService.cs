using SRS.Domain.Entities;

namespace SRS.Services.Interfaces.Bibliography
{
    public interface IBibliographyService<in TEntity>
        where TEntity : BaseEntity
    {
        string Get(TEntity entity);
    }
}
