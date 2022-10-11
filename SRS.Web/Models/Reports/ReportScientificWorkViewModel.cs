using System.Collections.Generic;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Reports
{
    public class ReportScientificWorkViewModel
    {
        public int Id { get; set; }

        public string ThemeOfScientificWorkDescription { get; set; }

        public IList<int> ThemeOfScientificWorkIds { get; set; }

        public IList<int> GrantIds { get; set; }

        public string ParticipationInGrands { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public IList<CheckboxListItem> StudentPublication { get; set; } = new List<CheckboxListItem>();
    }
}
