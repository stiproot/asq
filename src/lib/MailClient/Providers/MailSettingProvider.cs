
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MailClient.Providers
{
    public class MailSettingProvider : IMailSettingProvider
    {
        private readonly IConfiguration _config;

        public MailSettingProvider(IConfiguration config)
        {
            this._config = config;
        }

        public string GetServer()
        {
            return _config["Mail:Server"];
        }

        public string GetUsername()
        {
            return _config["Mail:Credentials:Username"];
        }

        public string GetPassword()
        {
            return _config["Mail:Credentials:Password"];
        }

        public short GetPort()
        {
            return short.Parse(_config["Mail:Port"]);

        }
        public string GetFromMailAddress()
        {
            return _config["Mail:FromMailAddress"];
        }

        public string GetTemplateDirPath()
        {
            return _config["Mail:TemplateDirPath"];
        }

        public string GetEmailConfirmationUrl()
        {
            return _config["Mail:EmailConfirmationUrl"];
        }

        public string GetMeetingGatewayUrl()
        {
            return _config["Mail:MeetingGatewayUrl"];
        }
    }
}