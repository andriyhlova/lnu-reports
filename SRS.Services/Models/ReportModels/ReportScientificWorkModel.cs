using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportScientificWorkModel : BaseModel
    {
        public int? ThemeOfScientificWorkId { get; set; }

        public string ThemeOfScientificWorkDescription { get; set; }
    }
}
