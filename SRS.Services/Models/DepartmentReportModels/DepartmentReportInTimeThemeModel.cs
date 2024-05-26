using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.DepartmentReportModels
{
    public class DepartmentReportInTimeThemeModel : BaseModel
    {
        public int? ThemeInWorkTimeId { get; set; }// 3

        public string AllDescriptionThemeInWorkTime { get; set; }

        public string CVThemeInWorkTime { get; set; }// 3.1

        public string DefensesOfCoworkersThemeInWorkTime { get; set; } // 3.2

        public string ApplicationAndPatentsOnInventionThemeInWorkTime { get; set; }// 3.4

        public string OtherThemeInWorkTime { get; set; }// 3.5

        public IList<int> PrintedPublicationThemeInWorkTimeIds { get; set; }// 3.3
    }
}
