using System.Threading.Tasks;

namespace TBS.Data.Interfaces.Notifications
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toName, string toEmail, string subject, string message);
    }
}
