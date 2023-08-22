using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportTemplateModel
    {
        public CathedraReportGeneralInfoModel GeneralInfo { get; set; }

        public IList<(string, IList<CathedraReportThemeOfScientificWorkModel>)> ThemeOfScientificWorks { get; set; }

        public CathedraReportPublicationsModel Publications { get; set; }

        public CathedraReportSignatureModel Signature { get; set; }
    }
}
