using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.DepartmentReport
{
    public class DepartmentReportGeneralInfoModel
    {
        public string Department { get; set; }

        public string DepartmentName { get; set; }

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

        public IList<DepartmentReportDissertarionDefenseModel> DissertationDefenseOfGraduates { get; set; }

        public IList<DepartmentReportDissertarionDefenseModel> DissertationDefenseOfEmployees { get; set; }

        public IList<DepartmentReportDissertarionDefenseModel> DissertationDefenseInAcademicCouncil { get; set; }
    }
}
