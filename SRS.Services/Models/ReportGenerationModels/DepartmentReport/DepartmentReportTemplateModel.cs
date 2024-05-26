using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.DepartmentReport
{
    public class DepartmentReportTemplateModel
    {
        public DepartmentReportGeneralInfoModel GeneralInfo { get; set; }

        public IList<(string, IList<DepartmentReportThemeOfScientificWorkModel>)> ThemeOfScientificWorks { get; set; }

        public DepartmentReportPublicationsModel Publications { get; set; }

        public DepartmentReportSignatureModel Signature { get; set; }

        public IList<string> Grants { get; set; }

        public string OtherGrantDescription { get; set; }
    }
}
