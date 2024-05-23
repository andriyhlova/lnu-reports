using SRS.Web.Models.Shared;
using System.Collections.Generic;

namespace SRS.Web.Models.CathedraReports
{
    public class CathedraReportGrantsViewModel
    {
        public int Id { get; set; }

        public IList<CheckboxListItem> Grants { get; set; }

        public string OtherGrants { get; set; }
    }
}
