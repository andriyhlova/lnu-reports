using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SRS.Domain.Entities
{
    public class Cathedra : BaseEntity
    {
        public string Name { get; set; }

        public string GenitiveCase { get; set; }

        [Column("Faculty_Id")]
        public int? FacultyId { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }
    }
}