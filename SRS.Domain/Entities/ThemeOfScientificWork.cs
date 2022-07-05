using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class ThemeOfScientificWork : BaseEntity
    {
        public string Value { get; set; }

        public string ThemeNumber { get; set; }

        public string Code { get; set; }

        public string ScientificHead { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodTo { get; set; }

        public Financial Financial { get; set; }

        public virtual ICollection<Report> Report { get; set; }

        public virtual Cathedra Cathedra { get; set; }
    }
}