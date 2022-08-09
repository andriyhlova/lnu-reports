using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportThemeOfScientificWorkModel
    {
        public string Title { get; set; }

        public string Number { get; set; }

        public string Code { get; set; }

        public string Head { get; set; }

        public string PeriodFrom { get; set; }

        public string PeriodTo { get; set; }

        public string Description { get; set; }

        public string ThemeCV { get; set; }

        public string DefensesOfCoworkers { get; set; }

        public List<CathedraReportPublicationCountersModel> PublicationsCounters { get; set; }

        public string ApplicationAndPatentsOnInvention { get; set; }

        public string Other { get; set; }
    }
}
