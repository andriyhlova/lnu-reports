using SRS.Services.Models.BaseModels;
using System.Collections.Generic;

namespace SRS.Services.Models.ReportModels
{
    public class CathedraReportOtherInfoModel : BaseModel
    {
        public string OtherFormsOfScientificWork { get; set; }// 5

        public string CooperationWithAcadamyOfScience { get; set; } // 6.1

        public string CooperationWithForeignScientificInstitution { get; set; } // 6.2

        public virtual IList<DissertationDefenseModel> DissertationDefenseOfGraduates { get; set; }// 7.1

        public virtual IList<DissertationDefenseModel> DissertationDefenseOfEmployees { get; set; }// 7.2

        public virtual IList<DissertationDefenseModel> DissertationDefenseInAcademicCouncil { get; set; }// 7.3

        public string StudentsWorks { get; set; }// 8

        public string ConferencesInUniversity { get; set; } // 10

        public string ApplicationOnInvention { get; set; }// 11.1

        public string Patents { get; set; }// 11.2

        public string Materials { get; set; }// 12

        public string PropositionForNewForms { get; set; } // 13
    }
}
