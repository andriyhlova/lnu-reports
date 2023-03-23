using SRS.Services.Attributes;
using SRS.Services.Models.UserModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Display(Name = "у тому числі статей у виданнях, включених до наукометричних баз даних Scopus / Web of Science")]
        public int InternationalMetricPublicationCounterBeforeRegistration { get; set; }

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
        [Display(Name = "Кількість інших наукових публікацій, що внесено до звітів за попередні роки")]
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

        [Display(Name = "Рік початку навчання в аспірантурі")]
        public int? AspirantStartYear { get; set; }

        [Display(Name = "Рік закінчення навчання в аспірантурі")]
        public int? AspirantFinishYear { get; set; }

        [Display(Name = "Рік захисту кандидатської дисертації/дисертації доктора філософії")]
        public int? DegreeDefenseYear { get; set; }

        [Display(Name = "Рік початку перебування в докторантурі")]
        public int? DoctorStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в докторантурі")]
        public int? DoctorFinishYear { get; set; }

        [Display(Name = "Рік захисту докторської дисертації")]
        public int? AcademicStatusDefenseYear { get; set; }

        [RequiredField]
        [Display(Name = "Посада")]
        public int? PositionId { get; set; }

        [Display(Name = "Посада")]
        public string PositionValue { get; set; }

        [Display(Name = "Web of Science ResearcherID")]
        public string ResearcherId { get; set; }

        [Display(Name = "ORCID")]
        public string Orcid { get; set; }

        [Display(Name = "Scopus Author ID")]
        public string ScopusAuthorId { get; set; }

        [Display(Name = "Google Scholar link")]
        public string GoogleScholarLink { get; set; }

        [Display(Name = "h-індекс (Scopus)")]
        public int? ScopusHIndex { get; set; }

        [Display(Name = "h-індекс (Web of Science)")]
        public int? WebOfScienceHIndex { get; set; }

        [Display(Name = "h-індекс (Google Scholar)")]
        public int? GoogleScholarHIndex { get; set; }

        public IList<UserDegreeViewModel> Degrees { get; set; }

        public IList<UserAcademicStatusViewModel> AcademicStatuses { get; set; }

        public IList<UserHonoraryTitleViewModel> HonoraryTitles { get; set; }
    }
}
