using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class Cathedra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }
    }
}