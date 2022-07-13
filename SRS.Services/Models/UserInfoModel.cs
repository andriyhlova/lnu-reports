using System;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models
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

        [Display(Name = "Рік захисту")]
        public DateTime? DefenseYear { get; set; }

        public int? CathedraId { get; set; }

        [Display(Name = "Кафедра")]
        public string CathedraName { get; set; }

        public int? FacultyId { get; set; }

        [Display(Name = "Факультет")]
        public string FacultyName { get; set; }

        public int? AcademicStatusId { get; set; }

        [Display(Name = "Науковий ступінь")]
        public string AcademicStatusName { get; set; }

        public int? ScienceDegreeId { get; set; }

        [Display(Name = "Вчене звання")]
        public string ScienceDegreeName { get; set; }

        public int? PositionId { get; set; }

        [Display(Name = "Посада")]
        public string PositionName { get; set; }
    }
}