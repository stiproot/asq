namespace MailClient.Providers
{
    public interface IMailSettingProvider
    {
        string GetServer();
        string GetUsername();
        string GetPassword();
        short GetPort();
        string GetFromMailAddress();
        string GetTemplateDirPath();
        string GetEmailConfirmationUrl();
        string GetMeetingGatewayUrl();
    }
}