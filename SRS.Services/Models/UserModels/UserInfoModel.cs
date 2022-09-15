using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models.UserModels
{
    public class UserInfoModel : BaseUserInfoModel
    {
        [Display(Name = "Дата народження")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Рік закінчення ЗВО")]
        public DateTime? GraduationDate { get; set; }

        [Display(Name = "Рік присвоєння вченого звання")]
        public DateTime? AwardingDate { get; set; }

        [Display(Name = "Рік початку перебування в аспірантурі")]
        public DateTime? AspirantStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в аспірантурі")]
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

        [Display(Name = "Наукові ступені")]
        public IList<UserDegreeModel> Degrees { get; set; }

        [Display(Name = "Вчені звання")]
        public IList<UserAcademicStatusModel> AcademicStatuses { get; set; }

        [Display(Name = "Почесні звання")]
        public IList<HonoraryTitleModel> HonoraryTitles { get; set; }
    }
}