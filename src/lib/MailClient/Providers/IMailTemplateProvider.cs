using DTO.Notification;

namespace MailClient.Providers
{
    public interface IMailTemplateProvider
    {
        string GetTemplateContents(NotificationTypeEnu typeEnu);
    }
}