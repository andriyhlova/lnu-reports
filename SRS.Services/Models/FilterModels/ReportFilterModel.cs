using System;

namespace SRS.Services.Models.FilterModels
{
    public class ReportFilterModel : DepartmentFilterModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }
    }
}
