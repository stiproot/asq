using Caching.Abstractions;
using Caching.Models;
using Caching.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Caching.Extensions
{
    public static class CachingServiceCollectionExtension
    {
        public static IServiceCollection AddCachingServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CacheSettings>(configuration.GetSection("Caching"));

            services.TryAddTransient<IMemoryCacheEntryOptionsFactory, MemoryCacheEntryOptionsFactory>();
            services.TryAddTransient<IMemoryCacheOptionsFactory, MemoryCacheOptionsFactory>();
            services.TryAddTransient<IMemoryCacheFactory, MemoryCacheFactory>();
            services.TryAddSingleton<ICache, Cache>();

            return services;
        }
    }
}