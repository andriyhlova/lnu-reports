using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.Report
{
    public class ReportSignatureModel
    {
        public string Protocol { get; set; }

        public string Date { get; set; }

        public List<string> CathedraLeadStatuses { get; set; }

        public string CathedraLead { get; set; }
    }
}
