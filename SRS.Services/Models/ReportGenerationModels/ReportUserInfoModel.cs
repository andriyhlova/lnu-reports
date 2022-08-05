namespace SRS.Services.Models.ReportGenerationModels
{
    public class ReportUserInfoModel
    {
        public string Position { get; set; }

        public string Cathedra { get; set; }

        public string UserFullName { get; set; }

        public int BirthYear { get; set; }

        public int GraduationYear { get; set; }

        public int? AspStart { get; set; }

        public int? AspFinish { get; set; }

        public int? DocStart { get; set; }

        public int? DocFinish { get; set; }

        public string ScientificDegree { get; set; }

        public int? ScientificDegreeYear { get; set; }

        public string AcademicStatus { get; set; }

        public int? AcademicStatusYear { get; set; }
    }
}
