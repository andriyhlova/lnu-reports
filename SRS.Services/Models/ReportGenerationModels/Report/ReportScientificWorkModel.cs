using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.Report
{
    public class ReportScientificWorkModel
    {
        public List<ReportThemeOfScientificWorkModel> ThemeOfScientificWorks { get; set; }

        public string OtherThemeOfScientificWorkDescription { get; set; }

        public List<ReportThemeOfScientificWorkModel> Grants { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public List<string> StudentPublications { get; set; }
    }
}
