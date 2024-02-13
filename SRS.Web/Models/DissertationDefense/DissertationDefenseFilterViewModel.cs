using SRS.Web.Models.Shared;
using System;

namespace SRS.Web.Models.DissertationDefense
{
    public class DissertationDefenseFilterViewModel : DepartmentFilterViewModel
    {
        public DateTime? PeriodFrom { get; set; }

        public DateTime? PeriodTo { get; set; }

        public string SupervisorId { get; set; }
    }
}