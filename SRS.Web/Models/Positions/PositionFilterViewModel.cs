using SRS.Domain.Enums;
using SRS.Web.Models.Shared;

namespace SRS.Web.Models.Position
{
    public class PositionFilterViewModel : BaseFilterViewModel
    {
        public PublicationType? PublicationType { get; set; }
    }
}