namespace SRS.Services.Models.FilterModels
{
    public class DepartmentFilterModel : BaseFilterModel
    {
        public int? CathedraId { get; set; }

        public int? FacultyId { get; set; }
    }
}
