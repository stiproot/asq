using dbaccess.Extensions;
using managers.Extensions;
using ZoomClient.Extensions;
using processes.Extensions;
using MailClient.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EventEngine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();

                    services
                        .AddDbAccessServices(null)
                        .AddResourceManagerServices(null)
                        .AddZoomClientServices(null)
                        .AddProcessesServices(null)
                        .AddMailClientServices(null);
                });
    }
}
