using SRS.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class DissertationDefense : BaseEntity
    {
        public string Theme { get; set; }

        public DateTime DefenseDate { get; set; }

        public DateTime SubmissionDate { get; set; }

        public string SupervisorId { get; set; }

        public string UserId { get; set; }

        public DissertationType? DissertationType { get; set; }

        public virtual ApplicationUser Supervisor { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}