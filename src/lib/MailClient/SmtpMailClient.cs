using MailClient.Providers;
using MailClient.Factories;
using DTO.Domain;
using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace MailClient
{
    public class SmtpMailClient: ISmtpMailClient
    {
        private readonly IMailSettingProvider _settings;
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly IMimeMessageBuilderFactory _mimeMessageBuilderFactory;

        public SmtpMailClient(
            IMailSettingProvider settings,
            ISmtpClientFactory smtpClientFactory,
            IMimeMessageBuilderFactory mimeMessageBuilderFactory
        )
        {
            this._settings = settings;
            this._smtpClientFactory = smtpClientFactory;
            this._mimeMessageBuilderFactory = mimeMessageBuilderFactory;
        }

        public async Task Send(MailDto dto)
        {
            MimeMessage message = this._mimeMessageBuilderFactory.Create().SetMailConfig(dto).Build();

            using(SmtpClient _smtpClient = this._smtpClientFactory.Create())
            {
                _smtpClient.Connect(this._settings.GetServer(), this._settings.GetPort(), useSsl:true);
                _smtpClient.Authenticate(this._settings.GetUsername(), this._settings.GetPassword());
                await _smtpClient.SendAsync(message);
            }
        }
    }
}
