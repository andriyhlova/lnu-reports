using SRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Domain.Entities
{
    public class ThemeOfScientificWorkSupervisor : BaseEntity
    {
        public string SupervisorId { get; set; }

        public virtual ApplicationUser Supervisor { get; set; }

        public int ThemeOfScientificWorkId { get; set; }

        public virtual ThemeOfScientificWork ThemeOfScientificWork { get; set; }

        public string GetSupervisor()
        {
            return Supervisor.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
        }
    }
}
