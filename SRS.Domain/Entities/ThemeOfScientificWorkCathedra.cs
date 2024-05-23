namespace SRS.Domain.Entities
{
    public class ThemeOfScientificWorkCathedra : BaseEntity
    {
        public int CathedraId { get; set; }

        public virtual Cathedra Cathedra { get; set; }

        public int ThemeOfScientificWorkId { get; set; }

        public virtual ThemeOfScientificWork ThemeOfScientificWork { get; set; }
    }
}