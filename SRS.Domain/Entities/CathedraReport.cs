using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class CathedraReport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string AchivementSchool { get; set; } // 1

        public virtual ThemeOfScientificWork BudgetTheme { get; set; }// 2

        public string AllDescriptionBudgetTheme { get; set; }

        public string CVBudgetTheme { get; set; }// 2.1

        public string DefensesOfCoworkersBudgetTheme { get; set; }// 2.2

        [InverseProperty("PrintedPublicationBudgetCathedraReport")]
        public virtual List<Publication> PrintedPublicationBudgetTheme { get; set; }// 2.3

        public string ApplicationAndPatentsOnInventionBudgetTheme { get; set; }// 2.4

        public string OtherBudgetTheme { get; set; }// 2.5

        public virtual ThemeOfScientificWork ThemeInWorkTime { get; set; }// 3

        public string AllDescriptionThemeInWorkTime { get; set; }

        public string CVThemeInWorkTime { get; set; }// 3.1

        public string DefensesOfCoworkersThemeInWorkTime { get; set; } // 3.2

        [InverseProperty("PrintedPublicationInWorkCathedraReport")]
        public virtual List<Publication> PrintedPublicationThemeInWorkTime { get; set; }// 3.3

        public string ApplicationAndPatentsOnInventionThemeInWorkTime { get; set; }// 3.4

        public string OtherThemeInWorkTime { get; set; }// 3.5

        public virtual ThemeOfScientificWork HospDohovirTheme { get; set; }// 4

        public string AllDescriptionHospDohovirTheme { get; set; }

        public string CVHospDohovirTheme { get; set; }// 4.1

        public string DefensesOfCoworkersHospDohovirTheme { get; set; }// 4.2

        [InverseProperty("PrintedPublicationHospDohovirCathedraReport")]
        public virtual List<Publication> PrintedPublicationHospDohovirTheme { get; set; }// 4.3

        public string ApplicationAndPatentsOnInventionHospDohovirTheme { get; set; }// 4.4

        public string OtherHospDohovirTheme { get; set; }// 4.5

        public string OtherFormsOfScientificWork { get; set; }// 5

        public string CooperationWithAcadamyOfScience { get; set; } // 6.1

        public string CooperationWithForeignScientificInstitution { get; set; } // 6.2

        public virtual List<CathedraDefenses> DefenseOfDoctorantsAndAspirants { get; set; }// 7.1

        public virtual List<CoworkersDefenses> DefenseOfCoworkers { get; set; }// 7.2

        public virtual List<OtherDefenses> DefenseWithSpecialPeople { get; set; }// 7.3

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

        public virtual ApplicationUser User { get; set; }

        public virtual List<Report> UserReport { get; set; }
    }
}