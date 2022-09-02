using ZoomClient;
using ZoomClient.Factory;
using ZoomClient.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Collections.Generic;
using System;

namespace ZoomClient.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build());

            services.AddScoped<IZoomHttpClient, ZoomHttpClient>();
            services.AddScoped<ISignatureBuilderFactory, SignatureBuilderFactory>();
            services.AddScoped<IZoomSettingProviderFactory, ZoomSettingProviderFactory>();
            services.AddScoped<IGenericHttpClient, GenericHttpClient>();
            services.AddScoped<IJwtBuilder, JwtBuilder>();
            services.AddScoped<IJwtBuilderFactory, JwtBuilderFactory>();
            services.AddScoped<IRequestBodyBuilder, RequestBodyBuilder>();
        }
    }
}