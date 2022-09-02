using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace dbaccess.Common
{
    public class StaticFileSettingProvider : IStaticFileSettingProvider
    {
        private readonly IConfiguration _config;

        public StaticFileSettingProvider(IConfiguration config) => this._config = config;

        public string GetImgBasePath() => _config["StaticFileServerSettings:ImgBasePath"];
        public string GetVideoBasePath() => _config["StaticFileServerSettings:VideoBasePath"];
        public async Task<string> GetImgBasePathAsync() => await Task.FromResult<string>("");
        public string GetStaticImgServerBaseUrl() => _config["StaticFileServerSettings:StaticImgServerBaseUrl"];
        public string GetStaticVideoServerBaseUrl() => _config["StaticFileServerSettings:StaticVideoServerBaseUrl"];
    }
}