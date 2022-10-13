namespace SRS.Domain.Entities
{
    public class ReportThemeOfScientificWork : BaseEntity
    {
        public string Description { get; set; }

        public int ReportId { get; set; }

        public virtual Report Report { get; set; }

        public int ThemeOfScientificWorkId { get; set; }

        public virtual ThemeOfScientificWork ThemeOfScientificWork { get; set; }
    }
}