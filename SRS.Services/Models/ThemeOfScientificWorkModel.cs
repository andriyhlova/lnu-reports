using System;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;
using SRS.Services.Attributes;

namespace SRS.Services.Models
{
    public class ThemeOfScientificWorkModel : BaseValueModel
    {
        [RequiredField]
        public string ThemeNumber { get; set; }

        public string Code { get; set; }

        [RequiredField]
        public string ScientificHead { get; set; }

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodFrom { get; set; }

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodTo { get; set; }

        [RequiredField]
        public Financial Financial { get; set; }

        [RequiredField]
        public int? CathedraId { get; set; }

        public string CathedraName { get; set; }

        public int? FacultyId { get; set; }
    }
}
