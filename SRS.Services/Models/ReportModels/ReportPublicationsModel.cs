using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportPublicationsModel : BaseModel
    {
        public IList<int> PrintedPublicationIds { get; set; }

        public IList<int> RecomendedPublicationIds { get; set; }

        public IList<int> AcceptedToPrintPublicationIds { get; set; }

        public IList<int> ApplicationsForInventionIds { get; set; }

        public IList<int> PatentsForInventionIds { get; set; }
    }
}
