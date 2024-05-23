using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models.UserModels
{
    public class UserInfoModel : BaseUserInfoModel
    {
        [Display(Name = "Кількість статей, що внесено до звітів за попередні роки")]
        public int PublicationCounterBeforeRegistration { get; set; }

        [Display(Name = "у тому числі статей у виданнях, включених до наукометричних баз даних Scopus / Web of Science")]
        public int InternationalMetricPublicationCounterBeforeRegistration { get; set; }

        [Display(Name = "Кількість монографій, що внесено до звітів за попередні роки")]
        public int MonographCounterBeforeRegistration { get; set; }

        [Display(Name = "Кількість підручників, що внесено до звітів за попередні роки")]
        public int BookCounterBeforeRegistration { get; set; }

        [Display(Name = "Кількість навчальних посібників, що внесено до звітів за попередні роки")]
        public int TrainingBookCounterBeforeRegistration { get; set; }

        [Display(Name = "Кількість інших наукових публікацій, що внесено до звітів за попередні роки")]
        public int OtherWritingCounterBeforeRegistration { get; set; }

        [Display(Name = "Кількість тез доповідей на конференціях, що внесено до звітів за попередні роки")]
        public int ConferenceCounterBeforeRegistration { get; set; }

        [Display(Name = "Кількість патентів, що внесено до звітів за попередні роки")]
        public int PatentCounterBeforeRegistration { get; set; }

        [Display(Name = "Дата народження")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Рік закінчення ЗВО")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Рік початку навчання в аспірантурі")]
        public DateTime? AspirantStartYear { get; set; }

        [Display(Name = "Рік закінчення навчання в аспірантурі")]
        public DateTime? AspirantFinishYear { get; set; }

        [Display(Name = "Рік захисту кандидатської дисертації/дисертації доктора філософії")]
        public DateTime? DegreeDefenseYear { get; set; }

        [Display(Name = "Рік початку перебування в докторантурі")]
        public DateTime? DoctorStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в докторантурі")]
        public DateTime? DoctorFinishYear { get; set; }

        [Display(Name = "Рік захисту докторської дисертації")]
        public DateTime? AcademicStatusDefenseYear { get; set; }

        public int? CathedraId { get; set; }

        [Display(Name = "Кафедра")]
        public string CathedraName { get; set; }

        public int? FacultyId { get; set; }

        [Display(Name = "Факультет")]
        public string FacultyName { get; set; }

        public int? PositionId { get; set; }

        [Display(Name = "Посада")]
        public string PositionName { get; set; }

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

        [Display(Name = "Наукові ступені")]
        public IList<UserDegreeModel> Degrees { get; set; }

        [Display(Name = "Вчені звання")]
        public IList<UserAcademicStatusModel> AcademicStatuses { get; set; }

        [Display(Name = "Почесні звання")]
        public IList<UserHonoraryTitleModel> HonoraryTitles { get; set; }
    }
}