using System;
using SRS.Domain.Enums;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Reports
{
    public class ReportFilterViewModel : DepartmentFilterViewModel
    {
        public ReportFilterViewModel()
        {
            HideSearch = true;
        }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }

        public ReportState? State { get; set; }
    }
}
