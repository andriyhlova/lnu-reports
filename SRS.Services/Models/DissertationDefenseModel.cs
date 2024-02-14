using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models
{
    public class DissertationDefenseModel : BaseModel
    {
        [RequiredField]
        public string Theme { get; set; }

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DefenseDate { get; set; } = DateTime.Now;

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        [RequiredField]
        public string SupervisorId { get; set; }

        public string SupervisorDescription { get; set; }

        [RequiredField]
        public string UserId { get; set; }

        public string UserDescription { get; set; }

        [RequiredField]
        public DissertationType? DissertationType { get; set; }
    }
}
