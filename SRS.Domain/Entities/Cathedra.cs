using System.Collections.Generic;

namespace SRS.Domain.Entities
{
    public class Cathedra : BaseEntity
    {
        public string Name { get; set; }

        public virtual Faculty Faculty { get; set; }

        public virtual ICollection<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }
    }
}