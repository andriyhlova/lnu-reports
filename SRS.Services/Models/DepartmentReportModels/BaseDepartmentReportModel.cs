using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Domain.Enums;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Models.DepartmentReportModels
{
    public class BaseDepartmentReportModel : BaseModel
    {
        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }

        public string DepartmentName { get; set; }

        public ReportState State { get; set; }

        public string UserId { get; set; }

        public IList<I18nUserInitialsModel> I18nUserInitials { get; set; }
    }
}
