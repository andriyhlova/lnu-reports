using SRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class ThemeOfScientificWorkSupervisorModel : BaseEntity
    {
        public int ThemeOfScientificWorkId { get; set; }

        public string SupervisorId { get; set; }

        public string SupervisorName { get; set; }
    }
}
