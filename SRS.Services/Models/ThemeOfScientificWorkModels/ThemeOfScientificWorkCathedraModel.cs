using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class ThemeOfScientificWorkCathedraModel : BaseModel
    {
        public int ThemeOfScientificWorkId { get; set; }

        public int CathedraId { get; set; }

        public string CathedraName { get; set; }

        public string FacultyName { get; set; }
    }
}