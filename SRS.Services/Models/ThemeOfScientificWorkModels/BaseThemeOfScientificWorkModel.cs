using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Models.BaseModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models.ThemeOfScientificWorkModels
{
    public class BaseThemeOfScientificWorkModel : BaseValueModel
    {
        public string ThemeNumber { get; set; }

        public string Code { get; set; }

        public string ScientificHead { get; set; }

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodFrom { get; set; } = DateTime.Now;

        [RequiredField]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PeriodTo { get; set; } = DateTime.Now;

        [RequiredField]
        public Financial Financial { get; set; }

        public ScientificWorkSubCategory? SubCategory { get; set; }

        public bool IsActive { get; set; }

        public decimal? PlannedAmount { get; set; }

        public Currency? Currency { get; set; }

        public string SupervisorId { get; set; }

        public string SupervisorDescription { get; set; }
    }
}
