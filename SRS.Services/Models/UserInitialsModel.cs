namespace SRS.Services.Models
{
    public class UserInitialsModel : BaseUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FathersName { get; set; }

        public string FullName => string.Join(" ", LastName, FirstName, FathersName);
    }
}