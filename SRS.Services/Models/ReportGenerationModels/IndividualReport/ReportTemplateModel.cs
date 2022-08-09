namespace SRS.Services.Models.ReportGenerationModels.IndividualReport
{
    public class ReportTemplateModel
    {
        public ReportGeneralInfoModel GeneralInfo { get; set; }

        public ReportUserInfoModel UserInfo { get; set; }

        public ReportThemeOfScientificWorkModel ThemeOfScientificWork { get; set; }

        public ReportPublicationCountersModel PublicationCounters { get; set; }

        public ReportPublicationsModel Publications { get; set; }

        public ReportSignatureModel Signature { get; set; }
    }
}
