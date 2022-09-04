using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportScientificWorkModel : BaseModel
    {
        public string ThemeOfScientificWorkDescription { get; set; }

        public IList<int> ThemeOfScientificWorkIds { get; set; }

        public IList<int> GrantIds { get; set; }

        public string ParticipationInGrands { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }
    }
}
