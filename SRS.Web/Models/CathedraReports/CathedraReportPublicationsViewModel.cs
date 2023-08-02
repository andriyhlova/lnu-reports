using SRS.Web.Models.Shared;
using System.Collections.Generic;

namespace SRS.Web.Models.CathedraReports
{
    public class CathedraReportPublicationsViewModel
    {
        public int Id { get; set; }

        public IList<CheckboxListItem> Publications { get; set; }

        public IList<CheckboxListItem> ApplicationsForInvention { get; set; }

        public IList<CheckboxListItem> PatentsForInvention { get; set; }
    }
}
