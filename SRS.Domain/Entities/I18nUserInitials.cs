using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S101:Types should be named in PascalCase", Justification = "Old name")]
    public class I18nUserInitials : BaseEntity
    {
        public Language Language { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FathersName { get; set; } = string.Empty;

        public virtual ApplicationUser User { get; set; }
    }
}