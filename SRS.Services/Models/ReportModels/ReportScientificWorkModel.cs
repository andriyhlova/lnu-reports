using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportScientificWorkModel : BaseModel
    {
        public IList<ReportThemeOfScientificWorkModel> ThemeOfScientificWorks { get; set; }

        public IList<ReportThemeOfScientificWorkModel> Grants { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public IList<int> StudentPublicationIds { get; set; }
    }
}
