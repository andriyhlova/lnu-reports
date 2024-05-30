namespace SRS.Services.Models.ReportGenerationModels.DepartmentReport
{
    public class DepartmentReportDissertarionDefenseModel
    {
        public string Theme { get; set; }

        public string DefenseDate { get; set; }

        public string SubmissionDate { get; set; }

        public string SpecializationName { get; set; }

        public string SupervisorDescription { get; set; }

        public string UserDescription { get; set; }

        public int YearOfGraduating { get; set; }

        public string DissertationType { get; set; }

        public string PositionAndCathedra { get; set; }
    }
}