using processes.Factory;
using processes.Engine;
using processes.Adapter;
using processes.Strategy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace processes.Extensions
{
    public static class ProcessesServiceCollectionExtension
    {
        public static IServiceCollection AddProcessesServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IProcessFactory, ProcessFactory>();
            services.TryAddSingleton<IEnginePacketFactory, EnginePacketFactory>();
            services.TryAddSingleton<ITaskRunnerFactory, TaskRunnerFactory>();
            services.TryAddSingleton<IStrategyFactoryFactory, StrategyFactoryFactory>();
            services.TryAddSingleton<IHandleExceptionStrategyFactoryFactory, HandleExeptionStrategyFactoryFactory>();
            services.TryAddSingleton<ITrackingAdapterFactory, TrackingAdapterFactory>();
            services.TryAddSingleton<ISharedContextFactory, SharedContextFactory>();

            return services;
        }
    }
}