using SRS.Web.Models.Shared;
using System.Collections.Generic;

namespace SRS.Web.Models.DepartmentReports
{
    public class DepartmentReportGrantsViewModel
    {
        public int Id { get; set; }

        public IList<CheckboxListItem> Grants { get; set; }

        public string OtherGrants { get; set; }
    }
}
