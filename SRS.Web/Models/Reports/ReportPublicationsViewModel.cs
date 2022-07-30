using SRS.Web.Models.Shared;
using System.Collections.Generic;

namespace SRS.Web.Models.Reports
{
    public class ReportPublicationsViewModel
    {
        public int Id { get; set; }
        public List<CheckboxListItem> PrintedPublication { get; set; } = new List<CheckboxListItem>();
        public List<CheckboxListItem> RecomendedPublication { get; set; } = new List<CheckboxListItem>();
        public List<CheckboxListItem> AcceptedToPrintPublication { get; set; } = new List<CheckboxListItem>();
    }
}
