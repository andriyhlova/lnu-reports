using SRS.Domain.Enums;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Journals
{
    public class JournalFilterViewModel : BaseFilterViewModel
    {
        public PublicationType? PublicationType { get; set; }
    }
}
