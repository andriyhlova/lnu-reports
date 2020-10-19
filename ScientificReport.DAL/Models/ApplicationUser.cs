using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ScientificReport.DAL.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Publication = new HashSet<Publication>();
        }

        public Int32 PublicationCounterBeforeRegistration { get; set; } = 0;
        public Int32 MonographCounterBeforeRegistration { get; set; } = 0;
        public Int32 BookCounterBeforeRegistration { get; set; } = 0;
        public Int32 TrainingBookCounterBeforeRegistration { get; set; } = 0;
        public Int32 OtherWritingCounterBeforeRegistration { get; set; } = 0;
        public Int32 ConferenceCounterBeforeRegistration { get; set; } = 0;
        public Int32 PatentCounterBeforeRegistration { get; set; } = 0;

        public Boolean IsActive { get; set; } = false;
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? GraduationDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? AwardingDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DefenseYear { get; set; }

        [DataType(DataType.Date)]
        public DateTime? AspirantStartYear { get; set; }
        [DataType(DataType.Date)]
        public DateTime? AspirantFinishYear { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoctorStartYear { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DoctorFinishYear { get; set; }

        public virtual Cathedra Cathedra { get; set; }
        public virtual AcademicStatus AcademicStatus { get; set; }
        public virtual ScienceDegree ScienceDegree { get; set; }
        public virtual Position Position { get; set; }
        public virtual ICollection<Publication> Publication { get; set; }
        public virtual ICollection<I18nUserInitials> I18nUserInitials { get; set; }
        public virtual ICollection<CathedraReport> CathedraReport { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
