using SRS.Web.Models.Shared;
using System.Collections.Generic;

namespace SRS.Web.Models.DepartmentReports
{
    public class DepartmentReportPublicationsViewModel
    {
        public int Id { get; set; }

        public IList<CheckboxListItem> Publications { get; set; }

        public IList<CheckboxListItem> ApplicationsForInvention { get; set; }

        public IList<CheckboxListItem> PatentsForInvention { get; set; }
    }
}
