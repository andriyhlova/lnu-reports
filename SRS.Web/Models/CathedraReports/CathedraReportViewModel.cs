using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.CathedraReports
{
    public class CathedraReportViewModel
    {
        public int Id { get; set; }

        public string AchivementSchool { get; set; } // 1

        public int? BudgetThemeId { get; set; }// 2

        public string AllDescriptionBudgetTheme { get; set; }

        public string CVBudgetTheme { get; set; }// 2.1

        public string DefensesOfCoworkersBudgetTheme { get; set; }// 2.2

        public string ApplicationAndPatentsOnInventionBudgetTheme { get; set; }// 2.4

        public string OtherBudgetTheme { get; set; }// 2.5

        public int? ThemeInWorkTimeId { get; set; }// 3

        public string AllDescriptionThemeInWorkTime { get; set; }

        public string CVThemeInWorkTime { get; set; }// 3.1

        public string DefensesOfCoworkersThemeInWorkTime { get; set; }// 3.2

        public string ApplicationAndPatentsOnInventionThemeInWorkTime { get; set; }// 3.4

        public string OtherThemeInWorkTime { get; set; }// 3.5

        public int? HospDohovirThemeId { get; set; }// 4

        public string AllDescriptionHospDohovirTheme { get; set; }

        public string CVHospDohovirTheme { get; set; }// 4.1

        public string DefensesOfCoworkersHospDohovirTheme { get; set; }// 4.2

        public string ApplicationAndPatentsOnInventionHospDohovirTheme { get; set; }// 4.4

        public string OtherHospDohovirTheme { get; set; }// 4.5

        public string OtherFormsOfScientificWork { get; set; }// 5

        public string CooperationWithAcadamyOfScience { get; set; } // 6.1

        public string CooperationWithForeignScientificInstitution { get; set; } // 6.2

        public string StudentsWorks { get; set; }// 8

        public string ConferencesInUniversity { get; set; } // 10

        public string ApplicationOnInvention { get; set; }// 11.1

        public string Patents { get; set; }// 11.2

        public string Materials { get; set; }// 12

        public string PropositionForNewForms { get; set; }// 13

        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        public IList<CheckboxListItem> PrintedPublicationBudgetTheme { get; set; }// 2.3

        public IList<CheckboxListItem> PrintedPublicationThemeInWorkTime { get; set; }// 3.3

        public IList<CheckboxListItem> PrintedPublicationHospDohovirTheme { get; set; }// 4.3
    }
}