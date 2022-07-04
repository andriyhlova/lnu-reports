using SRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagement.Models.Reports;

namespace UserManagement.Models.db
{
    public class Publication
    {
        public Publication()
        {
            this.User = new HashSet<ApplicationUser>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
        public string OtherAuthors { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime Date { get; set; }
        public string Pages { get; set; }
        public Double SizeOfPages { get; set; }
        public PublicationType PublicationType { get; set; }
        public Language Language { get; set; }
        public string Link { get; set; }
        public string Edition { get; set; }
        public string Magazine { get; set; }
        public string DOI { get; set; }
        public string Tome { get; set; }
        public string Place { get; set; }
        public string MainAuthor { get; set; }
        public string AuthorsOrder { get; set; }
        public bool IsMainAuthorRegistered { get; set; } = false;
        public virtual ICollection<ApplicationUser> User { get; set; }
        public virtual ICollection<Report> PrintedPublicationReport { get; set; }
        public virtual ICollection<Report> RecomendedPublicationReport { get; set; }
        public virtual ICollection<Report> AcceptedToPrintPublicationReport { get; set; }
        public virtual ICollection<CathedraReport> PrintedPublicationBudgetCathedraReport { get; set; }
        public virtual ICollection<CathedraReport> PrintedPublicationInWorkCathedraReport { get; set; }
        public virtual ICollection<CathedraReport> PrintedPublicationHospDohovirCathedraReport { get; set; }
    }
}