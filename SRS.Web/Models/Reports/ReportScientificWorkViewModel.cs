using System.Collections.Generic;
using SRS.Services.Models.ReportModels;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Reports
{
    public class ReportScientificWorkViewModel
    {
        public int Id { get; set; }

        public IList<ReportThemeOfScientificWorkModel> ThemeOfScientificWorks { get; set; }

        public string OtherThemeOfScientificWorkDescription { get; set; }

        public IList<ReportThemeOfScientificWorkModel> Grants { get; set; }

        public string OtherGrantDescription { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public IList<CheckboxListItem> StudentPublication { get; set; } = new List<CheckboxListItem>();
    }
}
