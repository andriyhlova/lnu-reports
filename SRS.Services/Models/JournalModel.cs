using System.Collections.Generic;
using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;

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

        public IList<JournalTypeModel> JournalTypes { get; set; }
    }
}