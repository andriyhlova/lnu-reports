namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class CathedraReportThemeOfScientificWorkModel : BaseThemeOfScientificWorkWithFinancialsModel
    {
        public string Resume { get; set; }

        public string DefendedDissertation { get; set; }

        public string Publications { get; set; }

        public decimal? FinancialAmount { get; set; }

        public int? FinancialYear { get; set; }
    }
}
