using System.Collections.Generic;
using SRS.Domain.Entities;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class CathedraModel : BaseModel
    {
        public string Name { get; set; }

        public string GenitiveCase { get; set; }

        public int? FacultyId { get; set; }

        public virtual FacultyModel Faculty { get; set; }

        public IList<ThemeOfScientificWork> ThemeOfScientificWorks { get; set; }
    }
}
