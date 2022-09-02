using MailClient.Providers;
using System;

namespace MailClient.Api
{
    public class MailClientApi : IMailClientApi
    {
        public IMailSettingProvider MailSettingProvider{ get; private set; }
        public IMailTemplateProvider MailTemplateProvider{ get; private set; }

        public MailClientApi(
            IMailSettingProvider mailSettingProvider,
            IMailTemplateProvider mailTemplateProvider
        )
        {
            this.MailSettingProvider = mailSettingProvider ?? throw new ArgumentNullException(nameof(mailSettingProvider));
            this.MailTemplateProvider = mailTemplateProvider ?? throw new ArgumentNullException(nameof(mailTemplateProvider));
        }
    }
}