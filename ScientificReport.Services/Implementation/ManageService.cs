using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.DAL;
using ScientificReport.DAL.Abstraction;
using ScientificReport.DAL.DTO;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;
using ScientificReport.Services.Abstraction;

namespace ScientificReport.Services.Implementation
{
    public class ManageService : IManageService
    {
        private IUnitOfWork db;
        public ManageService(IUnitOfWork db)
        {
            this.db = db;
        }

        public string FormStatusMessage(ManageMessageId? messageId)
        {
            return messageId == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : messageId == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : messageId == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : messageId == ManageMessageId.Error ? "An error has occurred."
                : messageId == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : messageId == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";
        }

        public IEnumerable<string> GetAcademicStatuses()
        {
            return db.AcademicStatuses.GetAllAsync().Result.ToList().Select(x => x.Value);
        }
        public IEnumerable<string> GetScienceDegrees()
        {
            return db.ScienceDegrees.GetAllAsync().Result.ToList().Select(x => x.Value);
        }
        public IEnumerable<string> GetPositions()
        {
            return db.Positions.GetAllAsync().Result.ToList().Select(x => x.Value);
        }

        public ApplicationUser UpdateUser(UpdateDTO model, string currentUserId, int? year, int? GraduationYear, int? DefenseDate, int? AwardingYear, int? AspirantStartYear, int? AspirantFinishYear, int? DoctorStartYear, int? DoctorFinishYear)
        {
            var user = db.Users.GetAllAsync().Result.First(x => x.Id == currentUserId);
            user.I18nUserInitials.Clear();
            user.BirthDate = model.BirthDate;
            user.AwardingDate = AwardingYear.HasValue ? new DateTime(AwardingYear.Value, 1, 1) : (DateTime?)null;
            user.GraduationDate = GraduationYear.HasValue ? new DateTime(GraduationYear.Value, 1, 1) : (DateTime?)null;
            user.DefenseYear = DefenseDate.HasValue ? new DateTime(DefenseDate.Value, 1, 1) : (DateTime?)null;
            user.AspirantStartYear = AspirantStartYear != null ? new DateTime(AspirantStartYear.Value, 1, 1) : (DateTime?)null;
            user.AspirantFinishYear = AspirantFinishYear != null ? new DateTime(AspirantFinishYear.Value, 1, 1) : (DateTime?)null;
            user.DoctorStartYear = DoctorStartYear != null ? new DateTime(DoctorStartYear.Value, 1, 1) : (DateTime?)null;
            user.DoctorFinishYear = DoctorFinishYear != null ? new DateTime(DoctorFinishYear.Value, 1, 1) : (DateTime?)null;

            user.PublicationCounterBeforeRegistration = model.PublicationsBeforeRegistration;
            user.MonographCounterBeforeRegistration = model.MonographCounterBeforeRegistration;
            user.BookCounterBeforeRegistration = model.BookCounterBeforeRegistration;
            user.TrainingBookCounterBeforeRegistration = model.TrainingBookCounterBeforeRegistration;
            user.OtherWritingCounterBeforeRegistration = model.OtherWritingCounterBeforeRegistration;
            user.ConferenceCounterBeforeRegistration = model.ConferenceCounterBeforeRegistration;
            user.PatentCounterBeforeRegistration = model.PatentCounterBeforeRegistration;

            user.AcademicStatus = db.AcademicStatuses.GetAllAsync().Result.First(x => x.Value == model.AcademicStatus);
            user.ScienceDegree = db.ScienceDegrees.GetAllAsync().Result.First(x => x.Value == model.ScienceDegree);
            user.Position = db.Positions.GetAllAsync().Result.First(x => x.Value == model.Position);
            user.I18nUserInitials = model.I18nUserInitials;
            db.SaveChanges();
            return user;
        }
    }
}
