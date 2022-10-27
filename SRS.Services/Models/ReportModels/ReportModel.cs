using System.Collections.Generic;

namespace SRS.Services.Models.ReportModels
{
    public class ReportModel : BaseReportModel
    {
        public string OtherThemeOfScientificWorkDescription { get; set; }

        public string OtherGrantDescription { get; set; }

        public string ScientificTrainings { get; set; } // Пункт 3

        public string ScientificControlDoctorsWork { get; set; } // Пункт 4

        public string ScientificControlStudentsWork { get; set; }// Пункт 5

        public string ApplicationForInevention { get; set; } // Пункт 7.1

        public string PatentForInevention { get; set; }// Пункт 7.2

        public string ReviewForTheses { get; set; }// Пункт 8

        public string MembershipInCouncils { get; set; } // Пункт 9

        public string Other { get; set; }// Пункт 10

        public IList<ReportThemeOfScientificWorkModel> ThemeOfScientificWorks { get; set; }

        public IList<int> StudentPublicationIds { get; set; }// Пункт 6.1

        public IList<int> PrintedPublicationIds { get; set; }// Пункт 6.1

        public IList<int> RecomendedPublicationIds { get; set; }// Пункт 6.2

        public IList<int> AcceptedToPrintPublicationIds { get; set; }// Пункт 6.2.5

        public IList<int> ApplicationsForInventionIds { get; set; }

        public IList<int> PatentsForInventionIds { get; set; }
    }
}
