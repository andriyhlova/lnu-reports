using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportGeneralInfoModel
    {
        public string Cathedra { get; set; }

        public int Year { get; set; }

        public string AchievementSchool { get; set; }

        public string OtherFormsOfScientificWork { get; set; }

        public string CooperationWithAcademyOfScience { get; set; }

        public string CooperationWithForeignScientificInstitution { get; set; }

        public string StudentsWorks { get; set; }

        public string ConferencesInUniversity { get; set; }

        public string ApplicationOnInvention { get; set; }

        public string Patents { get; set; }

        public string Materials { get; set; }

        public string PropositionForNewForms { get; set; }

        public IList<CathedraReportDissertarionDefenseModel> DissertationDefenseOfGraduates { get; set; }

        public IList<CathedraReportDissertarionDefenseModel> DissertationDefenseOfEmployees { get; set; }

        public IList<CathedraReportDissertarionDefenseModel> DissertationDefenseInAcademicCouncil { get; set; }
    }
}
