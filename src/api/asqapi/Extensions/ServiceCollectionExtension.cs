using asqapi.Providers;
using asqapi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace asqapi.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IAuthenticationService, AuthenticationService>();

            return services;
        }

        public static IServiceCollection AddProviderServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IClaimProvider, ClaimProvider>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<ITokenProvider, TokenProvider>();

            return services;
        }
    }
}