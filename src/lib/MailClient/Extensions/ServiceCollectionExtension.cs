using MailClient.Factories;
using MailClient.Providers;
using MailClient.Api;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MailClient.Extensions
{
    public static class MailServiceCollectionExtension
    {
        public static IServiceCollection AddMailClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IMimeMessageBuilderFactory, MimeMessageBuilderFactory>();
            services.TryAddTransient<ISmtpMailClient, SmtpMailClient>();
            services.TryAddTransient<ISmtpClientFactory, SmtpClientFactory>();
            services.TryAddTransient<IMailSettingProvider, MailSettingProvider>();
            services.TryAddTransient<IMailTemplateProvider, MailTemplateProvider>();
            //services.TryAddTransient<IMailSubjectProvider, MailSubjectProvider>();
            services.TryAddTransient<IMailClientApi, MailClientApi>();

            return services;
        }
    }
}