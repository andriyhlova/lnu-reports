using SRS.Services.Attributes;

namespace SRS.Services.Models.BaseModels
{
    public class BaseValueModel : BaseModel
    {
        [RequiredField]
        public string Value { get; set; }
    }
}
