using System;
using System.Collections.Generic;
using ScientificReport.DAL.Models;

namespace ScientificReport.DAL.DTO
{
    public class UpdateDTO
    {
        public string Email { get; set; }

        public ICollection<I18nUserInitials> I18nUserInitials { get; set; }

        public int PublicationsBeforeRegistration { get; set; }

        public int MonographCounterBeforeRegistration { get; set; } = 0;

        public int BookCounterBeforeRegistration { get; set; } = 0;

        public int TrainingBookCounterBeforeRegistration { get; set; } = 0;

        public int OtherWritingCounterBeforeRegistration { get; set; } = 0;

        public int ConferenceCounterBeforeRegistration { get; set; } = 0;

        public int PatentCounterBeforeRegistration { get; set; } = 0;

        public DateTime BirthDate { get; set; }

        public DateTime GraduationDate { get; set; }

        public DateTime AwardingDate { get; set; }

        public DateTime DefenseYear { get; set; }
        
        public int? AspirantStartYear { get; set; }
        
        public int? AspirantFinishYear { get; set; }

        public int? DoctorStartYear { get; set; }

        public int? DoctorFinishYear { get; set; }

        public string AcademicStatus { get; set; }

        public string ScienceDegree { get; set; }

        public string Position { get; set; }
    }
}
