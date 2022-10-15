using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class CathedraModel : BaseModel
    {
        public string Name { get; set; }

        public string GenitiveCase { get; set; }

        public int? FacultyId { get; set; }
    }
}
