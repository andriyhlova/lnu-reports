using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.Report
{
    public class ReportScientificWorkModel
    {
        public List<ReportThemeOfScientificWorkModel> ThemeOfScientificWorks { get; set; }

        public string ThemeOfScientificWorkDescription { get; set; }

        public List<ReportThemeOfScientificWorkModel> Grants { get; set; }

        public string ParticipationInGrands { get; set; }

        public bool HasGrants => Grants.Count > 0 || !string.IsNullOrEmpty(ParticipationInGrands);

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }
    }
}
