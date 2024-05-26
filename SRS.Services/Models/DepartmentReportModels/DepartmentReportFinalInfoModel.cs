using System;
using System.ComponentModel.DataAnnotations;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.DepartmentReportModels
{
    public class DepartmentReportFinalInfoModel : BaseModel
    {
        public string Protocol { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? Date { get; set; }
    }
}
