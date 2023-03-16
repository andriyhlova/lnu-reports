using System;
using System.Collections.Generic;

namespace SRS.Services.Models.UserModels
{
    public class ProfileInfoModel : BaseUserModel
    {
        public List<I18nUserInitialsModel> I18nUserInitials { get; set; }

        public int PublicationCounterBeforeRegistration { get; set; }

        public int InternationalMetricPublicationCounterBeforeRegistration { get; set; }

        public int MonographCounterBeforeRegistration { get; set; }

        public int BookCounterBeforeRegistration { get; set; }

        public int TrainingBookCounterBeforeRegistration { get; set; }

        public int OtherWritingCounterBeforeRegistration { get; set; }

        public int ConferenceCounterBeforeRegistration { get; set; }

        public int PatentCounterBeforeRegistration { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? GraduationDate { get; set; }

        public DateTime? AspirantStartYear { get; set; }

        public DateTime? AspirantFinishYear { get; set; }

        public DateTime? DegreeDefenseYear { get; set; }

        public DateTime? DoctorStartYear { get; set; }

        public DateTime? DoctorFinishYear { get; set; }

        public DateTime? AcademicStatusDefenseYear { get; set; }

        public string ResearcherId { get; set; }

        public string Orcid { get; set; }

        public string ScopusAuthorId { get; set; }

        public string GoogleScholarLink { get; set; }

        public int? ScopusHIndex { get; set; }

        public int? WebOfScienceHIndex { get; set; }

        public int? GoogleScholarHIndex { get; set; }

        public int? PositionId { get; set; }

        public string PositionValue { get; set; }

        public IList<UserDegreeModel> Degrees { get; set; }

        public IList<UserAcademicStatusModel> AcademicStatuses { get; set; }

        public IList<UserHonoraryTitleModel> HonoraryTitles { get; set; }
    }
}