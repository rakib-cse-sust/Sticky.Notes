using Sticky.Notes.Application.Models.Mail;
using System.Threading.Tasks;

namespace Sticky.Notes.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}
