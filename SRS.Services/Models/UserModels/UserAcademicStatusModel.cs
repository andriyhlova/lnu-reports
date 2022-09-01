using System;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.UserModels
{
    public class UserAcademicStatusModel : BaseModel
    {
        public DateTime AwardDate { get; set; }

        public string UserId { get; set; }

        public int AcademicStatusId { get; set; }

        public string AcademicStatusName { get; set; }
    }
}