using System;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.UserModels
{
    public class UserHonoraryTitleModel : BaseModel
    {
        public DateTime AwardDate { get; set; }

        public string UserId { get; set; }

        public int HonoraryTitleId { get; set; }

        public string HonoraryTitleName { get; set; }
    }
}