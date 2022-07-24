using SRS.Domain.Enums;
using SRS.Services.Attributes;

namespace SRS.Services.Models
{
    public class JournalModel : BaseModel
    {
        [RequiredField]
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string PrintIssn { get; set; }

        public string ElectronicIssn { get; set; }

        public Quartile BestQuartile { get; set; }
    }
}