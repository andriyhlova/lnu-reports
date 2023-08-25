namespace SRS.Domain.Entities
{
    public class Degree : BaseValueEntity
    {
        public int SortOrder { get; set; }

        public int Priority { get; set; }
    }
}