using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UserManagement.Models.db
{
    public class Cathedra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Name { get; set; }
       
        public virtual Faculty Faculty { get; set; }
        public virtual ICollection<ThemeOfScientificWork> ThemeOfScientificWork{ get; set; }
    }
}