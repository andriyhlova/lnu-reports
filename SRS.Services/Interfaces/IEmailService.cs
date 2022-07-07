using System.Threading.Tasks;

namespace SRS.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email, string subject, string htmlBody);
    }
}
