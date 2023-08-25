using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class AcademicStatus : BaseValueEntity
    {
        public int SortOrder { get; set; }

        public int Priority { get; set; }

        public AcademicStatusType Type { get; set; }
    }
}