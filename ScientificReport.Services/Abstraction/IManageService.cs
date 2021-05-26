using System.Collections.Generic;
using ScientificReport.DAL.DTO;
using ScientificReport.DAL.Enums;
using ScientificReport.DAL.Models;

namespace ScientificReport.Services.Abstraction
{
    public interface IManageService
    {
        string FormStatusMessage(ManageMessageId? messageId);
        IEnumerable<string> GetAcademicStatuses();
        IEnumerable<string> GetScienceDegrees();
        IEnumerable<string> GetPositions();
        ApplicationUser UpdateUser(UpdateDTO model, string currentUserId, int? year, int? GraduationYear, int? DefenseDate, int? AwardingYear, int? AspirantStartYear, int? AspirantFinishYear, int? DoctorStartYear, int? DoctorFinishYear);
    }
}