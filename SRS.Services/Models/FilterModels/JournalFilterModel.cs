using SRS.Domain.Enums;

namespace SRS.Services.Models.FilterModels
{
    public class JournalFilterModel : BaseFilterModel
    {
        public PublicationType? PublicationType { get; set; }
    }
}
