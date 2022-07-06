using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models
{
    public class BaseValueModel : BaseModel
    {
        [Required(ErrorMessage = "Введіть назву")]
        public string Value { get; set; }
    }
}
