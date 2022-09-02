using ZoomClient.Builder;
using ZoomClient.Factory;
using ZoomClient.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ZoomClient.Extensions
{
    public static class ZoomClientServiceCollectionExtension
    {
        public static IServiceCollection AddZoomClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IZoomHttpClient, ZoomHttpClient>();
            services.TryAddTransient<IGenericHttpClient, GenericHttpClient>();
            services.TryAddTransient<IJwtBuilder, JwtBuilder>();
            services.TryAddTransient<IJwtBuilderFactory, JwtBuilderFactory>();
            services.TryAddTransient<IRequestBodyBuilder, RequestBodyBuilder>();
            services.TryAddTransient<IZoomSettingProviderFactory, ZoomSettingProviderFactory>();
            services.TryAddTransient<IZoomSettingProvider, ZoomSettingProvider>();
            services.TryAddTransient<ISignatureBuilderFactory, SignatureBuilderFactory>();

            return services;
        }
    }
}