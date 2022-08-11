using System.Collections.Generic;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Reports
{
    public class ReportPublicationsViewModel
    {
        public int Id { get; set; }

        public IList<CheckboxListItem> PrintedPublication { get; set; } = new List<CheckboxListItem>();

        public IList<CheckboxListItem> RecomendedPublication { get; set; } = new List<CheckboxListItem>();

        public IList<CheckboxListItem> AcceptedToPrintPublication { get; set; } = new List<CheckboxListItem>();
    }
}
