using SRS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRS.Domain.Entities
{
    public class DissertationDefense : BaseEntity
    {
        public string Theme { get; set; }

        public DateTime DefenseDate { get; set; }

        public DateTime SubmissionDate { get; set; }

        public int? SpecializationId { get; set; }

        public string SupervisorId { get; set; }

        public string UserId { get; set; }

        public int YearOfGraduating { get; set; }

        public DissertationType? DissertationType { get; set; }

        public virtual ApplicationUser Supervisor { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Specialization Specialization { get; set; }

        public virtual ICollection<CathedraReport> CathedraReportDefenseOfGraduates { get; set; }

        public virtual ICollection<CathedraReport> CathedraReportDefenseOfEmployees { get; set; }

        public virtual ICollection<CathedraReport> CathedraReportDefenseInAcademicCouncil { get; set; }

        public virtual ICollection<FacultyReport> FacultyReportDefenseOfGraduates { get; set; }

        public virtual ICollection<FacultyReport> FacultyReportDefenseOfEmployees { get; set; }

        public virtual ICollection<FacultyReport> FacultyReportDefenseInAcademicCouncil { get; set; }

        public string GetSupervisor()
        {
            return Supervisor.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
        }

        public string GetUser()
        {
            return User.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA)?.FullName;
        }
    }
}