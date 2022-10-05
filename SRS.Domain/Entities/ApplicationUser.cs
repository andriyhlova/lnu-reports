using System;
using System.Collections.Generic;
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

        public int InternationalMetricPublicationCounterBeforeRegistration { get; set; }

        public int MonographCounterBeforeRegistration { get; set; }

        public int BookCounterBeforeRegistration { get; set; }

        public int TrainingBookCounterBeforeRegistration { get; set; }

        public int OtherWritingCounterBeforeRegistration { get; set; }

        public int ConferenceCounterBeforeRegistration { get; set; }

        public int PatentCounterBeforeRegistration { get; set; }

        public bool IsActive { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime? GraduationDate { get; set; }

        public DateTime? AspirantStartYear { get; set; }

        public DateTime? AspirantFinishYear { get; set; }

        public DateTime? DegreeDefenseYear { get; set; }

        public DateTime? DoctorStartYear { get; set; }

        public DateTime? DoctorFinishYear { get; set; }

        public DateTime? AcademicStatusDefenseYear { get; set; }

        public string ResearcherId { get; set; }

        public string Orcid { get; set; }

        public string ScopusAuthorId { get; set; }

        public string GoogleScholarLink { get; set; }

        public int? ScopusHIndex { get; set; }

        public int? WebOfScienceHIndex { get; set; }

        public int? GoogleScholarHIndex { get; set; }

        public string ApprovedById { get; set; }

        public virtual ApplicationUser ApprovedBy { get; set; }

        [Column("Cathedra_Id")]
        public int? CathedraId { get; set; }

        public virtual Cathedra Cathedra { get; set; }

        public virtual ICollection<ApplicationUserDegree> Degrees { get; set; }

        public virtual ICollection<ApplicationUserAcademicStatus> AcademicStatuses { get; set; }

        [Column("Position_Id")]
        public int? PositionId { get; set; }

        public virtual Position Position { get; set; }

        public virtual ICollection<Publication> Publication { get; set; }

        public virtual ICollection<ApplicationUserHonoraryTitle> HonoraryTitles { get; set; }

        public virtual ICollection<I18nUserInitials> I18nUserInitials { get; set; }

        public virtual ICollection<CathedraReport> CathedraReport { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}