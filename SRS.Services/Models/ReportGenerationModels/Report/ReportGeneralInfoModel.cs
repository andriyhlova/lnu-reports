namespace SRS.Services.Models.ReportGenerationModels.Report
{
    public class ReportGeneralInfoModel
    {
        public int Year { get; set; }

        public string ApplicationForInevention { get; set; }

        public string PatentForInevention { get; set; }

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
