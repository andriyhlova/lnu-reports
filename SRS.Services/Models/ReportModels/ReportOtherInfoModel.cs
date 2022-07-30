using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportOtherInfoModel : BaseModel
    {
        public string ParticipationInGrands { get; set; }

        public string ScientificTrainings { get; set; }

        public string ScientificControlDoctorsWork { get; set; }

        public string ScientificControlStudentsWork { get; set; }

        public string ApplicationForInevention { get; set; }

        public string PatentForInevention { get; set; }

        public string ReviewForTheses { get; set; }

        public string MembershipInCouncils { get; set; }

        public string Other { get; set; }
    }
}
