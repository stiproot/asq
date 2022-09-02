using dbaccess.Common;
using dbaccess.Factory;
using dbaccess.Repository;
using dbaccess.Mapper;
using dbaccess.Models;
using dbaccess.Factory.Test;
using dbaccess.Repository.QueryEnrichment;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace dbaccess.Extensions
{
    public static class DbAccessServiceCollectionExtension
    {
        public static IServiceCollection AddDbAccessServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IConnectionStringManager, ConnectionStringManager>();
            services.TryAddSingleton<IStaticFileSettingProvider, StaticFileSettingProvider>();
            services.TryAddSingleton<IAsqDbContextFactory<ASQContext>, AsqDbContextFactory>();
            services.TryAddSingleton<IGenericRepositoryFactory, GenericRespositoryFactory>();
            services.TryAddSingleton<IMapper>(new MapperConfiguration(config => 
            {
                config.AddProfile<EntityProfile>();
            }).CreateMapper());

            services.TryAddTransient<IUserResourceAccess, UserResourceAccess>();
            services.TryAddTransient<IAccountCreationTrackingResourceAccess, AccountCreationTrackingResourceAccess>();
            services.TryAddTransient<IFociResourceAccess, FociResourceAccess>();
            services.TryAddTransient<IImgResourceAccess, ImgResourceAccess>();
            services.TryAddTransient<ICardTypeResourceAccess, CardTypeResourceAccess>();
            services.TryAddTransient<IMeetingCreationTrackingResourceAccess, MeetingCreationTrackingResourceAccess>();
            services.TryAddTransient<IAccountUpdateTrackingResourceAccess, AccountUpdateTrackingResourceAccess>();
            services.TryAddTransient<IMeetingUpdateTrackingResourceAccess, MeetingUpdateTrackingResourceAccess>();
            services.TryAddTransient<IMeetingResourceAccess, MeetingResourceAccess>();
            services.TryAddTransient<ITimezoneResourceAccess, TimezoneResourceAccess>();
            services.TryAddTransient<IMailResourceAccess, MailResourceAccess>();
            services.TryAddTransient<IExtZoomMeetingWebHookResourceAccess, ExtZoomMeetingWebHookResourceAccess>();
            services.TryAddTransient<IBlogPostResourceAccess, BlogPostResourceAccess>();
            services.TryAddTransient<IMeetingRecordingDownloadTrackingResourceAccess, MeetingRecordingDownloadTrackingResourceAccess>();
            services.TryAddTransient<IExtZoomMeetingRecordingResourceAccess, ExtZoomMeetingRecordingResourceAccess>();
            services.TryAddTransient<INotificationResourceAccess, NotificationResourceAccess>();
            services.TryAddTransient<IMeetingUserMappingResourceAccess, MeetingUserMappingResourceAccess>();
            services.TryAddTransient<INotificationTrackingResourceAccess, NotificationTrackingResourceAccess>();
            services.TryAddTransient<IFocusUserMappingResourceAccess, FocusUserMappingResourceAccess>();
            services.TryAddTransient<IFocusHostMappingResourceAccess, FocusHostMappingResourceAccess>();
            services.TryAddTransient<IVideoResourceAccess, VideoResourceAccess>();
            services.TryAddTransient<IVidResourceAccess, VidResourceAccess>();

            services.TryAddTransient<IMeetingQueryEnrichmentResourceAccess, MeetingQueryEnrichmentResourceAccess>();
            services.TryAddTransient<IBlogPostQueryEnrichmentResourceAccess, BlogPostQueryEnrichmentResourceAccess>();
            services.TryAddTransient<IVideoQueryEnrichmentResourceAccess, VideoQueryEnrichmentResourceAccess>();
            services.TryAddTransient<IUserQueryEnrichmentResourceAccess, UserQueryEnrichmentResourceAccess>();

            // Test 
            services.TryAddTransient<IUserFactory, UserFactory>();
            services.TryAddTransient<ITestMeetingFactory, TestMeetingFactory>();

            return services;
        }
    }
}