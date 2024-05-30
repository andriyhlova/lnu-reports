using System;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.DepartmentReports
{
    public class DepartmentReportFilterViewModel : DepartmentFilterViewModel
    {
        public DepartmentReportFilterViewModel()
        {
            HideSearch = true;
        }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
