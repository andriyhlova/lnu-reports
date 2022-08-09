namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportTemplateModel
    {
        public CathedraReportGeneralInfoModel GeneralInfo { get; set; }

        public CathedraReportThemeOfScientificWorkModel BudgetThemeOfScientificWork { get; set; }

        public CathedraReportThemeOfScientificWorkModel InWorkTimeThemeOfScientificWork { get; set; }

        public CathedraReportThemeOfScientificWorkModel HospDohovirThemeOfScientificWork { get; set; }

        public CathedraReportPublicationsModel Publications { get; set; }

        public CathedraReportSignatureModel Signature { get; set; }
    }
}
