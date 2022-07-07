using SRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models.Account
{
    public class UpdateViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\\.)?[a-zA-Z]+\\.)?lnu\\.edu\\.ua", ErrorMessage = "Invalid email. Should be ...@lnu.edu.ua")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        public ICollection<I18nUserInitials> I18nUserInitials { get; set; }

        [Required]
        [Display(Name = "Кількість статей, що внесено до звітів за попередні роки")]
        public int PublicationsBeforeRegistration { get; set; }

        [Required]
        [Display(Name = "Кількість монографій, що внесено до звітів за попередні роки")]
        public int MonographCounterBeforeRegistration { get; set; } = 0;

        [Required]
        [Display(Name = "Кількість підручників, що внесено до звітів за попередні роки")]
        public int BookCounterBeforeRegistration { get; set; } = 0;

        [Required]
        [Display(Name = "Кількість навчальних посібників, що внесено до звітів за попередні роки")]
        public int TrainingBookCounterBeforeRegistration { get; set; } = 0;

        [Required]
        [Display(Name = "Кількість інших наукових видань, що внесено до звітів за попередні роки")]
        public int OtherWritingCounterBeforeRegistration { get; set; } = 0;

        [Required]
        [Display(Name = "Кількість тез доповідей на конференціях, що внесено до звітів за попередні роки")]
        public int ConferenceCounterBeforeRegistration { get; set; } = 0;

        [Required]
        [Display(Name = "Кількість патентів, що внесено до звітів за попередні роки")]
        public int PatentCounterBeforeRegistration { get; set; } = 0;

        [Required]
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Рік закінчення ЗВО")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime GraduationDate { get; set; }

        [Required]
        [Display(Name = "Рік присвоєння вченого звання")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime AwardingDate { get; set; }

        [Required]
        [Display(Name = "Рік захисту")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime DefenseYear { get; set; }

        [Display(Name = "Рік початку перебування в аспірантурі")]
        public int? AspirantStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в аспірантурі")]
        public int? AspirantFinishYear { get; set; }

        [Display(Name = "Рік початку перебування в докторатурі")]
        public int? DoctorStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в докторантурі")]
        public int? DoctorFinishYear { get; set; }

        [Required]
        [Display(Name = "Науковий ступінь")]
        public string AcademicStatus { get; set; }
        [Required]
        [Display(Name = "Вчене звання")]
        public string ScienceDegree { get; set; }
        [Required]
        [Display(Name = "Посада")]
        public string Position { get; set; }
    }
}
