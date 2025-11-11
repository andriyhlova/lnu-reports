using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportOtherInfoModel : BaseModel
    {
        public string ReviewForTheses { get; set; }

        public string MembershipInCouncils { get; set; }

        public string ApplicationsForInternationGrantsHorizonEurope { get; set; }

        public string ApplicationsForInternationGrantsErasmus { get; set; }

        public string ApplicationsForGrantsOfOtherFunds { get; set; }

        public string ApplicationsForNationwideCompetitionsOfNRFU { get; set; }

        public string ApplicationsForOtherCompetitions { get; set; }

        public string ExpertiseInInternationalCompetitions { get; set; }

        public string ExpertiseInNationwideCompetitiveSelections { get; set; }

        public string Other { get; set; }
    }
}
