using System.Collections.Generic;

namespace SRS.Domain.Entities
{
    public class Faculty : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Cathedra> Cathedra { get; set; }
    }
}