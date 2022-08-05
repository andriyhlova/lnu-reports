using System.ComponentModel.DataAnnotations.Schema;
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

        public string FullName => string.Join(" ", new[] { LastName, FirstName, FathersName });

        public string ShortFullName => string.Join(" ", new[] { LastName, !string.IsNullOrEmpty(FirstName) ? FirstName[0].ToString() + "." : string.Empty, !string.IsNullOrEmpty(FathersName) ? FathersName[0].ToString() + "." : string.Empty });

        public string ShortReverseFullName => string.Join(" ", new[] { !string.IsNullOrEmpty(FirstName) ? FirstName[0].ToString() + "." : string.Empty, !string.IsNullOrEmpty(FathersName) ? FathersName[0].ToString() + "." : string.Empty, LastName });

        [Column("User_Id")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}