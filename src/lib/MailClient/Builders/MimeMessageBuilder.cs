using DTO.Notification;
using DTO.Domain;
using MailClient.Providers;
using MimeKit;

namespace MailClient.Builders
{
    public class MimeMessageBuilder: IMimeMessageBuilder
    {
        private readonly IMailSettingProvider _settings;
        private readonly IMailTemplateProvider _templates;
        private MailDto _mailConfig;

        public MimeMessageBuilder(
            IMailSettingProvider settingProvider,
            IMailTemplateProvider mailTemplateProvider
        )
        {
            this._settings = settingProvider;
            this._templates = mailTemplateProvider;
        }

        public IMimeMessageBuilder SetMailConfig(MailDto config)
        {
            this._mailConfig = config;
            return this;
        }

        private MimeEntity Body(string htmlBody)
        {
            return new BodyBuilder()
            {
                HtmlBody = htmlBody
            }.ToMessageBody();
        }

        public MimeMessage Build()
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(this._settings.GetFromMailAddress()));
            message.To.Add(new MailboxAddress(this._mailConfig.ToEmailAddress));
            message.Subject = this._mailConfig.Subject;
            message.Body = this.Body(this._mailConfig.Body);

            return message;
        }
    }
}