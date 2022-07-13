using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models
{
    public class BaseUserModel
    {
        public string Id { get; set; }

        [Display(Name = "Активний")]
        public bool IsActive { get; set; }
    }
}
