using SRS.Services.Models.BaseModels;
using System.Collections.Generic;

namespace SRS.Services.Models.CathedraReportModels
{
    public class CathedraReportPublicationsModel : BaseModel
    {
        public IList<int> PublicationsIds { get; set; }

        public IList<int> ApplicationsForInventionIds { get; set; }

        public IList<int> PatentsForInventionIds { get; set; }
    }
}
