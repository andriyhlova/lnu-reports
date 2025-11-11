using SRS.Domain.Enums;
using System.Collections.Generic;

namespace SRS.Services.Models.FilterModels
{
    public class JournalFilterModel : BaseFilterModel
    {
        public PublicationType? PublicationType { get; set; }

        public List<Quartile?> Quartiles { get; set; } = new List<Quartile?>();
    }
}
