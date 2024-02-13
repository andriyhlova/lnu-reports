using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.FilterModels
{
    public class DissertationDefenseFilterModel : DepartmentFilterModel
    {
        public DateTime? PeriodFrom { get; set; }

        public DateTime? PeriodTo { get; set; }

        public string SupervisorId { get; set; }
    }
}
