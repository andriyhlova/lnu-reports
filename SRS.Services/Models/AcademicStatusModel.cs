using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class AcademicStatusModel : BaseValueModel
    {
        public int SortOrder { get; set; }

        public int Priority { get; set; }

        public AcademicStatusType Type { get; set; }
    }
}
