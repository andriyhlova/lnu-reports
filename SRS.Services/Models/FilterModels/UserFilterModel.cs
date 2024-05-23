namespace SRS.Services.Models.FilterModels
{
    public class UserFilterModel : DepartmentFilterModel
    {
        public bool? IsActive { get; set; }

        public string RoleId { get; set; }
    }
}
