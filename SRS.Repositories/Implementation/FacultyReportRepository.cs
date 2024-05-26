using SRS.Domain.Entities;
using SRS.Repositories.Context;
using SRS.Repositories.Implementations;

namespace SRS.Repositories.Implementation
{
    public class FacultyReportRepository : BaseRepository<FacultyReport>
    {
        public FacultyReportRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        protected override void AddRelatedEntities(FacultyReport entity)
        {
            AddCollection(entity.Publications);
            AddCollection(entity.ApplicationsForInvention);
            AddCollection(entity.PatentsForInvention);
            AddCollection(entity.Grants);
            AddCollection(entity.DissertationDefenseOfGraduates);
            AddCollection(entity.DissertationDefenseOfEmployees);
            AddCollection(entity.DissertationDefenseInAcademicCouncil);
        }

        protected override void UpdateRelatedEntities(FacultyReport existingEntity, FacultyReport newEntity)
        {
            UpdateCollection(existingEntity.Publications, newEntity.Publications);
            UpdateCollection(existingEntity.ApplicationsForInvention, newEntity.ApplicationsForInvention);
            UpdateCollection(existingEntity.PatentsForInvention, newEntity.PatentsForInvention);
            UpdateCollection(existingEntity.Grants, newEntity.Grants);
            UpdateCollection(existingEntity.DissertationDefenseOfGraduates, newEntity.DissertationDefenseOfGraduates);
            UpdateCollection(existingEntity.DissertationDefenseOfEmployees, newEntity.DissertationDefenseOfEmployees);
            UpdateCollection(existingEntity.DissertationDefenseInAcademicCouncil, newEntity.DissertationDefenseInAcademicCouncil);
        }
    }
}
