using System.Collections.Generic;
using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class JournalType : BaseValueEntity
    {
        public PublicationType? PublicationType { get; set; }

        public virtual ICollection<Journal> Journals { get; set; }
    }
}