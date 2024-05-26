using SRS.Services.Models.UserModels;
using System.Collections.Generic;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class DepartmentReportThemeOfScientificWorkModel : BaseThemeOfScientificWorkWithFinancialsModel
    {
        public string Resume { get; set; }

        public string DefendedDissertation { get; set; }

        public string Publications { get; set; }

        public decimal? FinancialAmount { get; set; }

        public int? FinancialYear { get; set; }

        public int? AmountOfApplicationUserFullTime { get; set; }

        public int? AmountOfApplicationUserExternalPartTime { get; set; }

        public int? AmountOfApplicationUserLawContract { get; set; }
    }
}
