﻿using SRS.Domain.Entities;
using SRS.Repositories.Context;

namespace SRS.Repositories.Implementations
{
    public class CathedraReportRepository : BaseRepository<CathedraReport>
    {
        public CathedraReportRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void AddRelatedEntities(CathedraReport entity)
        {
            AddCollection(entity.Publications);
            AddCollection(entity.ApplicationsForInvention);
            AddCollection(entity.PatentsForInvention);
            AddCollection(entity.Grants);
        }

        protected override void UpdateRelatedEntities(CathedraReport existingEntity, CathedraReport newEntity)
        {
            UpdateCollection(existingEntity.Publications, newEntity.Publications);
            UpdateCollection(existingEntity.ApplicationsForInvention, newEntity.ApplicationsForInvention);
            UpdateCollection(existingEntity.PatentsForInvention, newEntity.PatentsForInvention);
            UpdateCollection(existingEntity.Grants, newEntity.Grants);
        }
    }
}
