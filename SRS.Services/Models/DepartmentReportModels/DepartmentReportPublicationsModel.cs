using SRS.Services.Models.BaseModels;
using System.Collections.Generic;

namespace SRS.Services.Models.DepartmentReportModels
{
    public class DepartmentReportPublicationsModel : BaseModel
    {
        public IList<int> PublicationsIds { get; set; }

        public IList<int> ApplicationsForInventionIds { get; set; }

        public IList<int> PatentsForInventionIds { get; set; }
    }
}
