using System.Collections.Generic;

namespace SRS.Domain.Entities
{
    public class JournalType : BaseValueEntity
    {
        public virtual ICollection<Journal> Journals { get; set; }

        public virtual ICollection<JournalTypePublicationType> JournalTypePublicationTypes { get; set; }
    }
}