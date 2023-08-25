namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportThemeOfScientificWorkModel
    {
        public string Code { get; set; }

        public string Value { get; set; }

        public string SupervisorDescription { get; set; }

        public string ThemeNumber { get; set; }

        public string PeriodFrom { get; set; }

        public string PeriodTo { get; set; }

        public string Resume { get; set; }

        public string DefendedDissertation { get; set; }

        public string Publications { get; set; }

        public decimal? FinancialAmount { get; set; }

        public int? FinancialYear { get; set; }
    }
}
