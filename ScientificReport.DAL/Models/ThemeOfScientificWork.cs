using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScientificReport.DAL.Models
{
    public class ThemeOfScientificWork : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public String Value { get; set; }
        public String ThemeNumber { get; set; }
        public String Code { get; set; }
        public String ScientificHead { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime PeriodTo { get; set; }
        public Financial Financial { get; set; }
        public virtual ICollection<Report> Report { get; set; }
        public virtual Cathedra Cathedra { get; set; }
    }
}