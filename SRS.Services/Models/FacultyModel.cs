using System.Collections.Generic;
using SRS.Domain.Entities;
using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models
{
    public class FacultyModel : BaseModel
    {
        [RequiredField]
        public string Name { get; set; }

        public IList<Cathedra> Cathedra { get; set; }
    }
}
