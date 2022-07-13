namespace SRS.Services.Models.FilterModels
{
    public class UserFilterModel : BaseFilterModel
    {
        public int? CathedraId { get; set; }

        public int? FacultyId { get; set; }
    }
}
