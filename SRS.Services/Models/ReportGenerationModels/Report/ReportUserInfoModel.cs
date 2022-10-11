﻿using System.Collections.Generic;

namespace SRS.Services.Models.ReportGenerationModels.Report
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

        public List<ReportUserTitleModel> Degrees { get; set; }

        public List<ReportUserTitleModel> AcademicStatuses { get; set; }
    }
}