﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using UserManagement.Models.db;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data;
using UserManagement.Models.Reports;

namespace UserManagement.Models
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

        [Display(Name = "Активний")]
        public Boolean IsActive { get; set; } = false;
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
        [Display(Name = "Кафедра")]
        public virtual Cathedra Cathedra { get; set; }
        [Display(Name = "Науковий ступінь")]
        public virtual AcademicStatus AcademicStatus { get; set; }
        [Display(Name = "Вчене звання")]
        public virtual ScienceDegree ScienceDegree { get; set; }
        [Display(Name = "Позиція")]
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

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        [Display(Name = "Науковий ступінь")]
        public DbSet<AcademicStatus> AcademicStatus { get; set; }
        [Display(Name = "Вчене звання")]
        public DbSet<ScienceDegree> ScienceDegree { get; set; }
        [Display(Name = "Кафедра")]
        public DbSet<Cathedra> Cathedra { get; set; }
        [Display(Name = "Позиція")]
        public DbSet<Position> Position { get; set; }
        [Display(Name = "Факультет")]
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Publication> Publication { get; set; }
        public DbSet<ThemeOfScientificWork> ThemeOfScientificWork { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<UserManagement.Models.db.Report> Reports { get; set; }
        public System.Data.Entity.DbSet<UserManagement.Models.Reports.CathedraReport> CathedraReport{ get; set; }
    }
}