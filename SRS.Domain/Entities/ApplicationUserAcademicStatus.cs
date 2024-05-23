using System;

namespace SRS.Domain.Entities
{
    public class ApplicationUserAcademicStatus : BaseEntity
    {
        public DateTime AwardDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int AcademicStatusId { get; set; }

        public virtual AcademicStatus AcademicStatus { get; set; }
    }
}