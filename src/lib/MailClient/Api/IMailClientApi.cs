using MailClient.Providers;

namespace MailClient.Api
{
    public interface IMailClientApi
    {
        IMailTemplateProvider MailTemplateProvider{ get; }
        IMailSettingProvider MailSettingProvider{ get; }
    }
}