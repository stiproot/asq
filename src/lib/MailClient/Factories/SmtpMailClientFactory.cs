using MailKit.Net.Smtp;

namespace MailClient.Factories
{
    public class SmtpClientFactory: ISmtpClientFactory
    {
        public SmtpClient Create()
        {
            return new SmtpClient();
        }
    }
}