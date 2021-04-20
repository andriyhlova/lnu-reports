using ScientificReport.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScientificReport.DAL.Models
{
    public class CoworkersDefenses : IBaseEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 Id { get; set; }
        public String SurnameAndInitials { get; set; }
        public String PositionAndCathedra { get; set; }
        public String Spetiality { get; set; }
        public DateTime DateOfDefense { get; set; }
        public String ThemeOfWork { get; set; }
    }
}