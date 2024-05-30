using SRS.Domain.Enums;
using SRS.Services.Attributes;
using SRS.Services.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.ReportGenerationModels.CathedraReport
{
    public class CathedraReportDissertarionDefenseModel
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