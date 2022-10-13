using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class Report : BaseEntity
    {
        public string ScientificTrainings { get; set; } // Пункт 3

        public string ScientificControlDoctorsWork { get; set; } // Пункт 4

        public string ScientificControlStudentsWork { get; set; }// Пункт 5

        public string ApplicationForInevention { get; set; } // Пункт 7.1

        public string PatentForInevention { get; set; }// Пункт 7.2

        public string ReviewForTheses { get; set; }// Пункт 8

        public string MembershipInCouncils { get; set; } // Пункт 9

        public string Other { get; set; }// Пункт 10

        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        public ReportState State { get; set; }

        [Column("User_Id")]
        public string UserId { get; set; }

        public virtual ICollection<ReportThemeOfScientificWork> ThemeOfScientificWorks { get; set; }// Пункт 1

        public virtual ApplicationUser User { get; set; }

        [InverseProperty("StudentPublicationReport")]
        public virtual ICollection<Publication> StudentPublication { get; set; }// Пункт 6.1

        [InverseProperty("PrintedPublicationReport")]
        public virtual ICollection<Publication> PrintedPublication { get; set; }// Пункт 6.1

        [InverseProperty("RecomendedPublicationReport")]
        public virtual ICollection<Publication> RecomendedPublication { get; set; }// Пункт 6.2

        [InverseProperty("AcceptedToPrintPublicationReport")]
        public virtual ICollection<Publication> AcceptedToPrintPublication { get; set; }// Пункт 6.2.5

        [InverseProperty("ApplicationsForInventionReport")]
        public virtual ICollection<Publication> ApplicationsForInvention { get; set; }

        [InverseProperty("PatentsForInventionReport")]
        public virtual ICollection<Publication> PatentsForInvention { get; set; }

        public virtual List<CathedraReport> CathedraReport { get; set; }
    }
}