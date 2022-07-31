using SRS.Domain.Enums;

namespace SRS.Services.Models.FilterModels
{
    public class CathedraReportPublicationFilterModel
    {
        public Financial Financial { get; set; }

        public int CathedraId { get; set; }
    }
}
