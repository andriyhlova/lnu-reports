using System;

namespace SRS.Services.Models.FilterModels
{
    public class CathedraReportFilterModel : DepartmentFilterModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
