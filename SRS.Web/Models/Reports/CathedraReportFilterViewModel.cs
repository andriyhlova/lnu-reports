using SRS.Web.Models.Shared;
using System;

namespace SRS.Web.Models.Reports
{
    public class CathedraReportFilterViewModel : DepartmentFilterViewModel
    {
        public CathedraReportFilterViewModel()
        {
            HideSearch = true;
        }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
