namespace SRS.Domain.Entities
{
    public class ThemeOfScientificWorkFinancial : BaseEntity
    {
        public int Year { get; set; }

        public decimal Amount { get; set; }

        public int ThemeOfScientificWorkId { get; set; }

        public virtual ThemeOfScientificWork ThemeOfScientificWork { get; set; }
    }
}