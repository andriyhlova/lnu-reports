using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Attributes
{
    public class RequiredFieldAttribute : RequiredAttribute
    {
        public RequiredFieldAttribute()
        {
            this.ErrorMessage = "Це поле є обов'язковим";
        }
    }
}
