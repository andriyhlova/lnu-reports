using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class DegreeModel : BaseValueModel
    {
        public int SortOrder { get; set; }

        public int Priority { get; set; }
    }
}
