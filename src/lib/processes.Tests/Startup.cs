using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using dbaccess.Extensions;
using processes.Extensions;
using ZoomClient.Extensions;
using MailClient.Extensions;

namespace processes.Tests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);

            services.AddDbAccessServices(configuration);
            services.AddProcessesServices(configuration);
            services.AddZoomClientServices(configuration);
            services.AddMailClientServices(configuration);
        }
    }
}