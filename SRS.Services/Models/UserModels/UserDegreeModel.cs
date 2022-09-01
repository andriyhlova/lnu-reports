using System;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.UserModels
{
    public class UserDegreeModel : BaseModel
    {
        public DateTime AwardDate { get; set; }

        public string UserId { get; set; }

        public int DegreeId { get; set; }

        public string DegreeName { get; set; }
    }
}