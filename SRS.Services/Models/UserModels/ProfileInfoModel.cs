using System;
using System.Collections.Generic;
using SRS.Services.Models.BaseModels;

namespace SRS.Services.Models.UserModels
{
    public class ProfileInfoModel : BaseUserModel
    {
        public List<I18nUserInitialsModel> I18nUserInitials { get; set; }

        public int PublicationCounterBeforeRegistration { get; set; }

        public int MonographCounterBeforeRegistration { get; set; }

        public int BookCounterBeforeRegistration { get; set; }

        public int TrainingBookCounterBeforeRegistration { get; set; }

        public int OtherWritingCounterBeforeRegistration { get; set; }

        public int ConferenceCounterBeforeRegistration { get; set; }

        public int PatentCounterBeforeRegistration { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? GraduationDate { get; set; }

        public DateTime? AwardingDate { get; set; }

        public DateTime? DefenseYear { get; set; }

        public DateTime? AspirantStartYear { get; set; }

        public DateTime? AspirantFinishYear { get; set; }

        public DateTime? DoctorStartYear { get; set; }

        public DateTime? DoctorFinishYear { get; set; }

        public int? DegreeId { get; set; }

        public int? AcademicStatusId { get; set; }

        public int? PositionId { get; set; }
    }
}