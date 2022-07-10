using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SRS.Domain.Entities
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Publication = new HashSet<Publication>();
        }

        public int PublicationCounterBeforeRegistration { get; set; }

        public int MonographCounterBeforeRegistration { get; set; }

        public int BookCounterBeforeRegistration { get; set; }

        public int TrainingBookCounterBeforeRegistration { get; set; }

        public int OtherWritingCounterBeforeRegistration { get; set; }

        public int ConferenceCounterBeforeRegistration { get; set; }

        public int PatentCounterBeforeRegistration { get; set; }

        [Display(Name = "Активний")]
        public bool IsActive { get; set; }

        [DataType(DataType.Date)]
        [Display(Name ="Дата народження")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік закінчення ЗВО")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? GraduationDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік присвоєння вченого звання")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AwardingDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік захисту")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DefenseYear { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік початку перебування в аспірантурі")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AspirantStartYear { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік закінчення перебування в аспірантурі")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? AspirantFinishYear { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік початку перебування в докторатурі")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DoctorStartYear { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Рік закінчення перебування в докторантурі")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]

        public DateTime? DoctorFinishYear { get; set; }

        public string ApprovedById { get; set; }

        public virtual ApplicationUser ApprovedBy { get; set; }

        [Column("Cathedra_Id")]
        public int? CathedraId { get; set; }

        [Display(Name = "Кафедра")]
        public virtual Cathedra Cathedra { get; set; }

        [Column("AcademicStatus_Id")]
        public int? AcademicStatusId { get; set; }

        [Display(Name = "Науковий ступінь")]
        public virtual AcademicStatus AcademicStatus { get; set; }

        [Column("ScienceDegree_Id")]
        public int? ScienceDegreeId { get; set; }

        [Display(Name = "Вчене звання")]
        public virtual ScienceDegree ScienceDegree { get; set; }

        [Column("Position_Id")]
        public int? PositionId { get; set; }

        [Display(Name = "Позиція")]
        public virtual Position Position { get; set; }

        public virtual ICollection<Publication> Publication { get; set; }

        public virtual ICollection<I18nUserInitials> I18nUserInitials { get; set; }

        public virtual ICollection<CathedraReport> CathedraReport { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}