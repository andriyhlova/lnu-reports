using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SRS.Domain.Entities
{
    public class ReportThemeOfScientificWork : BaseEntity
    {
        public string Description { get; set; }

        public string Resume { get; set; }

        public string Publications { get; set; }

        public string DefendedDissertation { get; set; }

        public int ReportId { get; set; }

        public virtual Report Report { get; set; }

        public int ThemeOfScientificWorkId { get; set; }

        public virtual ThemeOfScientificWork ThemeOfScientificWork { get; set; }

        [InverseProperty("ReportUserFullTime")]
        public virtual ICollection<ApplicationUser> ApplicationUserFullTime { get; set; }

        [InverseProperty("ReportUserExternalPartTime")]
        public virtual ICollection<ApplicationUser> ApplicationUserExternalPartTime { get; set; }

        [InverseProperty("ReportUserLawContract")]
        public virtual ICollection<ApplicationUser> ApplicationUserLawContract { get; set; }
    }
}