using SRS.Domain.Entities;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class ThemeOfScientificWorkSupervisorModel : BaseEntity
    {
        public int ThemeOfScientificWorkId { get; set; }

        public string SupervisorId { get; set; }

        public string SupervisorName { get; set; }
    }
}
