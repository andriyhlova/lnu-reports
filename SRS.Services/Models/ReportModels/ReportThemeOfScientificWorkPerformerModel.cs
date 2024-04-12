using SRS.Domain.Entities;

namespace SRS.Services.Models.ReportModels
{
    public class ReportThemeOfScientificWorkPerformerModel : BaseEntity
    {
        public int ReportThemeOfScientificWorkId { get; set; }

        public string PerformerId { get; set; }

        public string PerformerName { get; set; }
    }
}
