using System;
using Microsoft.Extensions.Configuration;
using ZoomClient.Builder;
using ZoomClient.Providers;

namespace ZoomClient.Factory
{
    public class ZoomSettingProviderFactory: IZoomSettingProviderFactory
    {
        private readonly IConfiguration _config;

        public ZoomSettingProviderFactory(IConfiguration config)
        {
            this._config = config;
        }

        public IZoomSettingProvider Create()
        {
            return new ZoomSettingProvider(this._config);
        }
    }
}