using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class ReportPublicationsModel : BaseModel
    {
        public IList<int> PrintedPublication { get; set; }

        public IList<int> RecomendedPublication { get; set; }

        public IList<int> AcceptedToPrintPublication { get; set; }
    }
}
