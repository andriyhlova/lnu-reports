namespace SRS.Services.Models
{
    public class UserInitialsModel : BaseUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FathersName { get; set; }

        public int? CathedraId { get; set; }

        public int? FacultyId { get; set; }
    }
}