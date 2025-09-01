using System.Threading.Tasks;

namespace Okean_AnimeMovie.Application.Services.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlMessage);
    }
}


