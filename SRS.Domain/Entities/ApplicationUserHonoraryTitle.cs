using System;

namespace SRS.Domain.Entities
{
    public class ApplicationUserHonoraryTitle : BaseEntity
    {
        public DateTime AwardDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int HonoraryTitleId { get; set; }

        public virtual HonoraryTitle HonoraryTitle { get; set; }
    }
}