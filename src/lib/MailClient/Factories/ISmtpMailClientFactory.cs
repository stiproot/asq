using MailKit.Net.Smtp;

namespace MailClient.Factories
{
    public interface ISmtpClientFactory
    {
        SmtpClient Create();
    }
}