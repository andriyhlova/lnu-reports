namespace SRS.Services.Models.ReportGenerationModels.Report
{
    public class ReportThemeOfScientificWorkModel
    {
        public string Theme { get; set; }

        public string Description { get; set; }

        public int AmountOfApplicationUserFullTime { get; set; }

        public int AmountOfApplicationUserExternalPartTime { get; set; }

        public int AmountOfApplicationUserLawContract { get; set; }
    }
}
