namespace ZoomClient.Providers
{
    public interface IZoomSettingProvider
    {
        string GetApiSecret();
        string GetApiKey();
        long GetEpochConstant();
        string GetWebHookVerificationToken();
    }
}