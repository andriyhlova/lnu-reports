using System.Collections.Generic;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.CathedraReports
{
    public class CathedraReportBudgetThemeViewModel
    {
        public int Id { get; set; }

        public int? BudgetThemeId { get; set; }// 2

        public string AllDescriptionBudgetTheme { get; set; }

        public string CVBudgetTheme { get; set; }// 2.1

        public string DefensesOfCoworkersBudgetTheme { get; set; }// 2.2

        public string ApplicationAndPatentsOnInventionBudgetTheme { get; set; }// 2.4

        public string OtherBudgetTheme { get; set; }// 2.5

        public IList<CheckboxListItem> PrintedPublicationBudgetTheme { get; set; }// 2.3
    }
}
