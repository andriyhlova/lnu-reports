using SRS.Services.Models.ReportModels;
using System.Collections.Generic;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class CathedraReportThemeOfScientificWorkModel : BaseThemeOfScientificWorkModel
    {
        public IList<ThemeOfScientificWorkFinancialModel> ThemeOfScientificWorkFinancials { get; set; }

        public ReportThemeOfScientificWorkModel ReportThemeOfScientificWork { get; set; }
    }
}
