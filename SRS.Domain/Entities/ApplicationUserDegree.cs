using System;

namespace SRS.Domain.Entities
{
    public class ApplicationUserDegree : BaseEntity
    {
        public DateTime AwardDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int DegreeId { get; set; }

        public virtual Degree Degree { get; set; }
    }
}