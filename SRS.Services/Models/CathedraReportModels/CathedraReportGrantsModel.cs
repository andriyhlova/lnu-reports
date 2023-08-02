using SRS.Services.Models.BaseModels;
using System.Collections.Generic;

namespace SRS.Services.Models.CathedraReportModels
{
    public class CathedraReportGrantsModel : BaseModel
    {
        public IList<int> GrantsIds { get; set; }

        public string OtherGrants { get; set; }
    }
}
