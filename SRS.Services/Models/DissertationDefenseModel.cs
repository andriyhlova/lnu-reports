using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Extensions;
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
        public int? SpecializationId { get; set; }

        public SpecializationModel Specialization { get; set; }

        public string SpecializationName { get; set; }

        public string SpecializationCode { get; set; }

        [RequiredField]
        public string SupervisorId { get; set; }

        public string SupervisorDescription { get; set; }

        [RequiredField]
        public string UserId { get; set; }

        public string UserDescription { get; set; }

        [YearRange(1900, ErrorMessage = "Значення має бути в межах від 1900 до теперішнього року")]
        public int YearOfGraduating { get; set; }

        [RequiredField]
        public DissertationType? DissertationType { get; set; }
    }
}
