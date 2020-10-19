using ScientificReport.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScientificReport.DAL.Models
{
    public class CathedraDefenses : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public String SurnameAndInitials { get; set; }
        public String ScientificHead { get; set; }
        public Int32 YearOfEnd { get; set; }
        public DateTime DateOfInning { get; set; }
        public DateTime DateOfDefense { get; set; }
        public String ThemeOfWork { get; set; }
    }
}