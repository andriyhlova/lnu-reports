using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class FacultyModel : BaseModel
    {
        [RequiredField]
        public string Name { get; set; }
    }
}
