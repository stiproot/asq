using System;
using Microsoft.Extensions.Configuration;
using ZoomClient.Builder;
using ZoomClient.Factory;
using ZoomClient.Providers;

namespace ZoomClient.Factory
{
    public class SignatureBuilderFactory : ISignatureBuilderFactory
    {
        private readonly IZoomSettingProviderFactory _settingProviderFactory;

        public SignatureBuilderFactory(IZoomSettingProviderFactory settingProviderFactory) 
        {
            this._settingProviderFactory = settingProviderFactory;
        }

        public ISignatureBuilder Create()
        {
            return new SignatureBuilder(this._settingProviderFactory.Create());
        }
    }
}