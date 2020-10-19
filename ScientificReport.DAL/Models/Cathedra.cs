using ScientificReport.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ScientificReport.DAL.Models
{
    public class Cathedra : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 ID { get; set; }
        public String Name { get; set; }
       
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<ThemeOfScientificWork> ThemeOfScientificWork{ get; set; }
    }
}