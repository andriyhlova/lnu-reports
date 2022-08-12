using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class ThemeOfScientificWork : BaseValueEntity
    {
        public string ThemeNumber { get; set; }

        public string Code { get; set; }

        public string ScientificHead { get; set; }

        public DateTime PeriodFrom { get; set; }

        public DateTime PeriodTo { get; set; }

        public Financial Financial { get; set; }

        public ScientificWorkSubCategory? SubCategory { get; set; }

        public virtual ICollection<Report> Report { get; set; }

        [Column("Cathedra_Id")]
        public int? CathedraId { get; set; }

        public virtual Cathedra Cathedra { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}