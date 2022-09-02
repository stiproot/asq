using DTO.Domain;
using System.Threading.Tasks;

namespace MailClient
{
    public interface ISmtpMailClient
    {
        Task Send(MailDto message);
    }
}
