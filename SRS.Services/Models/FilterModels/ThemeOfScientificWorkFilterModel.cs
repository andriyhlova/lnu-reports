using SRS.Domain.Enums;

namespace SRS.Services.Models.FilterModels
{
    public class ThemeOfScientificWorkFilterModel : BaseFilterModel
    {
        public Financial? Financial { get; set; }

        public bool? IsActive { get; set; }
    }
}
