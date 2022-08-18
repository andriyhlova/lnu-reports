using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.Report
{
    public class ReportTemplateModel
    {
        public ReportGeneralInfoModel GeneralInfo { get; set; }

        public ReportUserInfoModel UserInfo { get; set; }

        public List<ReportThemeOfScientificWorkModel> ThemeOfScientificWorks { get; set; }

        public ReportPublicationCountersModel PublicationCounters { get; set; }

        public ReportPublicationsModel Publications { get; set; }

        public ReportSignatureModel Signature { get; set; }
    }
}
