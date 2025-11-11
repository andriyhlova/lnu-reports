using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class JournalTypePublicationType : BaseEntity
    {
        public PublicationType? PublicationType { get; set; }

        public int JournalTypeId { get; set; }

        public virtual JournalType JournalType { get; set; }
    }
}