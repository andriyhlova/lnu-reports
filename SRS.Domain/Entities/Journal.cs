using System.Collections.Generic;
using SRS.Domain.Enums;

namespace SRS.Domain.Entities
{
    public class Journal : BaseEntity
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public string PrintIssn { get; set; }

        public string ElectronicIssn { get; set; }

        public Quartile? BestQuartile { get; set; }

        public virtual ICollection<JournalType> JournalTypes { get; set; }
    }
}