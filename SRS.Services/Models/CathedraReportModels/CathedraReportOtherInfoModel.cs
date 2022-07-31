using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.ReportModels
{
    public class CathedraReportOtherInfoModel : BaseModel
    {
        public string AchivementSchool { get; set; }

        public string OtherFormsOfScientificWork { get; set; }// 5

        public string CooperationWithAcadamyOfScience { get; set; } // 6.1

        public string CooperationWithForeignScientificInstitution { get; set; } // 6.2

        public string StudentsWorks { get; set; }// 8

        public string ConferencesInUniversity { get; set; } // 10

        public string ApplicationOnInvention { get; set; }// 11.1

        public string Patents { get; set; }// 11.2

        public string Materials { get; set; }// 12

        public string PropositionForNewForms { get; set; } // 13
    }
}
