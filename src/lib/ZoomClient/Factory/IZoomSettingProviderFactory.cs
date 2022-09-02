using System;
using Microsoft.Extensions.Configuration;
using ZoomClient.Builder;
using ZoomClient.Providers;

namespace ZoomClient.Factory
{
    public interface IZoomSettingProviderFactory
    {
        IZoomSettingProvider Create();
    }
}