using SRS.Domain.Enums;
using System;
using System.Linq;

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

        public string GetSupervisor()
        {
            return Supervisor.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
        }

        public string GetUser()
        {
            return User.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
        }
    }
}