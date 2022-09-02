using MimeKit;
using MailClient.Builders;
using MailClient.Providers;

namespace MailClient.Factories
{
    public class MimeMessageBuilderFactory: IMimeMessageBuilderFactory
    {
        private readonly IMailSettingProvider _settings;
        private readonly IMailTemplateProvider _templates;

        public MimeMessageBuilderFactory(
            IMailSettingProvider settings,
            IMailTemplateProvider templates
        )
        {
            this._settings = settings;
            this._templates = templates;
        }

        public IMimeMessageBuilder Create()
        {
            return new MimeMessageBuilder(this._settings, this._templates);
        }
    }
}