using Microsoft.Extensions.Configuration;

namespace ZoomClient.Providers
{
    public class ZoomSettingProvider : IZoomSettingProvider
    {
        private readonly IConfiguration _configuration;

        public ZoomSettingProvider(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        public string GetApiSecret()
            => this._configuration["Zoom:WebApi:Config:ApiSecret"];

        public string GetApiKey()
            => this._configuration["Zoom:WebApi:Config:ApiKey"];

        public long GetEpochConstant()
            => long.Parse(this._configuration["Zoom:WebApi:Config:EpochConstant"]);

        public string GetWebHookVerificationToken()
            => this._configuration["Zoom:WebHook:VerificationToken"];
    }
}