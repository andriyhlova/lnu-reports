using SRS.Web.Models.Shared;
using System;

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
    }
}
