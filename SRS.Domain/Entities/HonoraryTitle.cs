using System.Collections.Generic;

namespace SRS.Domain.Entities
{
    public class HonoraryTitle : BaseValueEntity
    {
        public int SortOrder { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}