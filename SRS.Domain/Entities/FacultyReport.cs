using SRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class FacultyReport : BaseEntity
    {
        // leave
        public string AchivementSchool { get; set; } // 1

        [Column("BudgetTheme_Id")]
        public int? BudgetThemeId { get; set; }// 2

        public string AllDescriptionBudgetTheme { get; set; }

        public string CVBudgetTheme { get; set; }// 2.1

        public string DefensesOfCoworkersBudgetTheme { get; set; }// 2.2

        public string ApplicationAndPatentsOnInventionBudgetTheme { get; set; }// 2.4

        public string OtherBudgetTheme { get; set; }// 2.5

        [Column("ThemeInWorkTime_Id")]
        public int? ThemeInWorkTimeId { get; set; }// 3

        public string AllDescriptionThemeInWorkTime { get; set; }

        public string CVThemeInWorkTime { get; set; }// 3.1

        public string DefensesOfCoworkersThemeInWorkTime { get; set; } // 3.2

        public string ApplicationAndPatentsOnInventionThemeInWorkTime { get; set; }// 3.4

        public string OtherThemeInWorkTime { get; set; }// 3.5

        [Column("HospDohovirTheme_Id")]
        public int? HospDohovirThemeId { get; set; }// 3

        public string AllDescriptionHospDohovirTheme { get; set; }

        public string CVHospDohovirTheme { get; set; }// 4.1

        public string DefensesOfCoworkersHospDohovirTheme { get; set; }// 4.2

        public string ApplicationAndPatentsOnInventionHospDohovirTheme { get; set; }// 4.4

        public string OtherHospDohovirTheme { get; set; }// 4.5

        // leave
        public string OtherFormsOfScientificWork { get; set; }// 5

        // leave
        public string CooperationWithAcadamyOfScience { get; set; } // 6.1

        // leave
        public string CooperationWithForeignScientificInstitution { get; set; } // 6.2

        // leave
        public string StudentsWorks { get; set; }// 8

        // leave
        public string ConferencesInUniversity { get; set; } // 10

        public string ApplicationOnInvention { get; set; }// 11.1

        public string Patents { get; set; }// 11.2

        // leave
        public string Materials { get; set; }// 12

        // leave
        public string PropositionForNewForms { get; set; }// 13

        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        public ReportState State { get; set; }

        public string OtherGrants { get; set; }

        [Column("User_Id")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual List<Report> UserReport { get; set; }

        public virtual ThemeOfScientificWork BudgetTheme { get; set; }// 2

        public virtual ThemeOfScientificWork ThemeInWorkTime { get; set; }// 3

        public virtual ThemeOfScientificWork HospDohovirTheme { get; set; }// 4

        [InverseProperty("PublicationsFacultyReport")]
        public virtual List<Publication> Publications { get; set; }

        [InverseProperty("PatentsForInventionFacultyReport")]
        public virtual ICollection<Publication> PatentsForInvention { get; set; }// Пункт 6.2.5

        [InverseProperty("ApplicationsForInventionFacultyReport")]
        public virtual ICollection<Publication> ApplicationsForInvention { get; set; }

        [InverseProperty("FacultyReports")]
        public virtual ICollection<ThemeOfScientificWork> Grants { get; set; }

        [InverseProperty("FacultyReportDefenseOfGraduates")]
        public virtual ICollection<DissertationDefense> DissertationDefenseOfGraduates { get; set; }// 7.1

        [InverseProperty("FacultyReportDefenseOfEmployees")]
        public virtual ICollection<DissertationDefense> DissertationDefenseOfEmployees { get; set; }// 7.2

        [InverseProperty("FacultyReportDefenseInAcademicCouncil")]
        public virtual ICollection<DissertationDefense> DissertationDefenseInAcademicCouncil { get; set; }// 7.3
    }
}
