using System;
using SRS.Domain.Enums;

namespace SRS.Services.Models.FilterModels
{
    public class ReportFilterModel : DepartmentFilterModel
    {
        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public string UserId { get; set; }

        public ReportState? State { get; set; }
    }
}
