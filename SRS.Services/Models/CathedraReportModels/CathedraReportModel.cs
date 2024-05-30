using SRS.Domain.Entities;
using SRS.Services.Models.CathedraReportModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Services.Models.ReportModels
{
    public class CathedraReportModel : BaseCathedraReportModel
    {
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

        public string DefensesOfCoworkersThemeInWorkTime { get; set; } // 3.2

        public string ApplicationAndPatentsOnInventionThemeInWorkTime { get; set; }// 3.4

        public string OtherThemeInWorkTime { get; set; }// 3.5

        public int? HospDohovirThemeId { get; set; }// 3

        public string AllDescriptionHospDohovirTheme { get; set; }

        public string CVHospDohovirTheme { get; set; }// 4.1

        public string DefensesOfCoworkersHospDohovirTheme { get; set; }// 4.2

        public string ApplicationAndPatentsOnInventionHospDohovirTheme { get; set; }// 4.4

        public string OtherHospDohovirTheme { get; set; }// 4.5

        public string OtherFormsOfScientificWork { get; set; }// 5

        public string CooperationWithAcadamyOfScience { get; set; } // 6.1

        public string CooperationWithForeignScientificInstitution { get; set; } // 6.2

        public virtual IList<DissertationDefenseModel> DissertationDefenseOfGraduates { get; set; }// 7.1

        public virtual IList<DissertationDefenseModel> DissertationDefenseOfEmployees { get; set; }// 7.2

        public virtual IList<DissertationDefenseModel> DissertationDefenseInAcademicCouncil { get; set; }// 7.3

        public string StudentsWorks { get; set; }// 8

        public string ConferencesInUniversity { get; set; } // 10

        public string ApplicationOnInvention { get; set; }// 11.1

        public string Patents { get; set; }// 11.2

        public string Materials { get; set; }// 12

        public string PropositionForNewForms { get; set; } // 13

        public int CathedraId { get; set; }

        public List<int> PublicationsIds { get; set; }

        public IList<int> ApplicationsForInventionIds { get; set; }

        public IList<int> PatentsForInventionIds { get; set; }

        public IList<int> GrantsIds { get; set; }

        public string OtherGrants { get; set; }
    }
}
