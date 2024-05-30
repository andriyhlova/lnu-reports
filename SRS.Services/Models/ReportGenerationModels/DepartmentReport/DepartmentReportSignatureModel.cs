using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.DepartmentReport
{
    public class DepartmentReportSignatureModel
    {
        public string Faculty { get; set; }

        public string Protocol { get; set; }

        public string Date { get; set; }

        public List<string> FacultyLeadStatuses { get; set; }

        public string FacultyLead { get; set; }
    }
}
