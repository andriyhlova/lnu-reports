using SRS.Domain.Enums;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.ThemeOfScientificWorks
{
    public class ThemeOfScientificWorkFilterViewModel : BaseFilterViewModel
    {
        public Financial? Financial { get; set; }

        public bool? IsActive { get; set; }
    }
}
