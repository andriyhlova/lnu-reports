using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.DepartmentReportModels
{
    public class DepartmentReportBudgetThemeModel : BaseModel
    {
        public int? BudgetThemeId { get; set; }// 2

        public string AllDescriptionBudgetTheme { get; set; }

        public string CVBudgetTheme { get; set; }// 2.1

        public string DefensesOfCoworkersBudgetTheme { get; set; }// 2.2

        public string ApplicationAndPatentsOnInventionBudgetTheme { get; set; }// 2.4

        public string OtherBudgetTheme { get; set; }// 2.5

        public IList<int> PrintedPublicationBudgetThemeIds { get; set; }// 2.3
    }
}
