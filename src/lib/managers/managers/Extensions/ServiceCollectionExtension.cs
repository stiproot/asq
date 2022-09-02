using managers.Resource;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace managers.Extensions
{
    public static class ResourceManagersServiceCollectionExtension
    {
        public static IServiceCollection AddResourceManagerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient<IAccountResourceManager, AccountResourceManager>();
            services.TryAddTransient<IUserResourceManager, UserResourceManager>();
            services.TryAddTransient<IImgResourceManager, ImgResourceManager>();
            services.TryAddTransient<ILookupResourceManager, LookupResourceManager>();
            services.TryAddTransient<IMeetingResourceManager, MeetingResourceManager>();
            services.TryAddTransient<IZoomResourceManager, ZoomResourceManager>();
            services.TryAddTransient<IBlogPostResourceManager, BlogPostResourceManager>();
            services.TryAddTransient<INotificationResourceManager, NotificationResourceManager>();
            services.TryAddTransient<IVideoResourceManager, VideoResourceManager>();

            return services;
        }
    }
}