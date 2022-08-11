using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Services.Attributes;
using SRS.Services.Models.UserModels;

namespace SRS.Web.Models.Account
{
    public class UpdateProfileViewModel
    {
        public string Id { get; set; }

        [Required]
        public List<I18nUserInitialsModel> I18nUserInitials { get; set; }

        [RequiredField]
        [Display(Name = "Кількість статей, що внесено до звітів за попередні роки")]
        public int PublicationCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Кількість монографій, що внесено до звітів за попередні роки")]
        public int MonographCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Кількість підручників, що внесено до звітів за попередні роки")]
        public int BookCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Кількість навчальних посібників, що внесено до звітів за попередні роки")]
        public int TrainingBookCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Кількість інших наукових видань, що внесено до звітів за попередні роки")]
        public int OtherWritingCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Кількість тез доповідей на конференціях, що внесено до звітів за попередні роки")]
        public int ConferenceCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Кількість патентів, що внесено до звітів за попередні роки")]
        public int PatentCounterBeforeRegistration { get; set; }

        [RequiredField]
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Рік закінчення ЗВО")]
        public int? GraduationDate { get; set; }

        [Display(Name = "Рік присвоєння вченого звання")]
        public int? AwardingDate { get; set; }

        [Display(Name = "Рік захисту")]
        public int? DefenseYear { get; set; }

        [Display(Name = "Рік початку перебування в аспірантурі")]
        public int? AspirantStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в аспірантурі")]
        public int? AspirantFinishYear { get; set; }

        [Display(Name = "Рік початку перебування в докторатурі")]
        public int? DoctorStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в докторантурі")]
        public int? DoctorFinishYear { get; set; }

        [RequiredField]
        [Display(Name = "Науковий ступінь")]
        public int? DegreeId { get; set; }

        [RequiredField]
        [Display(Name = "Вчене звання")]
        public int? AcademicStatusId { get; set; }

        [RequiredField]
        [Display(Name = "Посада")]
        public int? PositionId { get; set; }
    }
}
