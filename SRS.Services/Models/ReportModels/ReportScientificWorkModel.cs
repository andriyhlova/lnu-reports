using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportScientificWorkModel : BaseModel
    {
        public string ThemeOfScientificWorkDescription { get; set; }

        public IList<int> ThemeOfScientificWorkIds { get; set; }
    }
}
