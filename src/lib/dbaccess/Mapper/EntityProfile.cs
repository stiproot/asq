using dbaccess.Models;
using DTO.Domain;
using DTO.Tracking;
using DTO.Domain.Ext.Zoom;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace dbaccess.Mapper
{
    public class EntityProfile : Profile 
    {
        public EntityProfile()
        {
            #region [Blog]

            CreateMap<TbFocusBlogPostMapping, FocusBlogPostMappingDto>();
            CreateMap<FocusBlogPostMappingDto, TbFocusBlogPostMapping>()
                .ForMember(dest => dest.Focus, opt => opt.MapFrom(src => (TbFocus)null));

            CreateMap<TbBlogPost, BlogPostDto>()
                .ForMember(dest => dest.Foci, opt => opt.MapFrom(src => src.TbFocusBlogPostMappings));
            CreateMap<BlogPostDto, TbBlogPost>()
                .ForMember(dest => dest.TbFocusBlogPostMappings, opt => opt.MapFrom(src => src.Foci))
                .ForMember(dest => dest.CreationUser, opt => opt.MapFrom(src => (TbUser)null));

            // summary
            CreateMap<TbBlogPost, BlogPostSummaryDto>()
                .ForMember(dest => dest.CreationUserUniqueId, opt => opt.MapFrom(src => src.CreationUser.UniqueId))
                .ForMember(dest => dest.CreationUserThumbnailUrl, opt => opt.MapFrom(src => src.CreationUser.Img.ThumbnailUrl))
                .ForMember(dest => dest.CreationUserName, opt => opt.MapFrom(src => src.CreationUser.Name))
                .ForMember(dest => dest.CreationUserSurname, opt => opt.MapFrom(src => src.CreationUser.Surname))
                .ForMember(dest => dest.CreationUserUsername, opt => opt.MapFrom(src => src.CreationUser.Username))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.Img.ThumbnailUrl));

            #endregion [Blog]

            #region [User]

            CreateMap<AccountTypeEnu, TbAccountType>()
                .ConvertUsing(src => new TbAccountType{ Id = (short)src});

            CreateMap<TbFocus, FocusDto>()
                .ForMember(dest => dest.Id, opt=> opt.MapFrom(src => (long)src.Id));
            CreateMap<FocusDto, TbFocus>()
                .ForMember(dest => dest.Id, opt=> opt.MapFrom(src => (short)src.Id));
            
            CreateMap<TbFocusUserMapping, FocusUserMappingDto>();
            CreateMap<FocusUserMappingDto, TbFocusUserMapping>()
                .ForMember(dest => dest.Focus, opt => opt.MapFrom(src => (TbFocus)null));

            CreateMap<TbUser, UserDto>()
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => (AccountTypeEnu)src.AccountTypeId))
                .ForMember(dest => dest.Interests, opt => opt.MapFrom(src => src.TbFocusUserMappings));
            CreateMap<UserDto, TbUser>()
                .ForMember(dest => dest.AccountType, opt => opt.MapFrom(src => (TbAccountType)null))
                .ForMember(dest => dest.AccountTypeId, opt => opt.MapFrom(src => (short)src.AccountType))
                .ForMember(dest => dest.TbFocusUserMappings, opt => opt.MapFrom(src => src.Interests));

            CreateMap<TbFocusHostMapping, FocusHostMappingDto>();
            CreateMap<FocusHostMappingDto, TbFocusHostMapping>()
                .ForMember(dest => dest.Focus, opt => opt.MapFrom(src => (TbFocus)null));

            CreateMap<TbHost, HostDto>()
                .ForMember(dest => dest.Specialities, opt => opt.MapFrom(src => src.TbFocusHostMappings));
            CreateMap<HostDto, TbHost>()
                .ForMember(dest => dest.TbFocusHostMappings, opt => opt.MapFrom(src => src.Specialities))
                .ForMember(dest => dest.TbUsers, opt => opt.MapFrom(src => (ICollection<TbUser>)null));

            CreateMap<TbCardType, CardTypeDto>();
            CreateMap<CardTypeDto, TbCardType>();

            CreateMap<PaymentInfoDto, TbPaymentInfo>();
            CreateMap<TbPaymentInfo, PaymentInfoDto>();

            CreateMap<TbAccountCreationTracking, AccountCreationTrackingDto>();
            CreateMap<AccountCreationTrackingDto, TbAccountCreationTracking>();

            CreateMap<TbAccountUpdateTracking, AccountUpdateTrackingDto>();
            CreateMap<AccountUpdateTrackingDto, TbAccountUpdateTracking>();

            CreateMap<TbExtZoomUser, ExtZoomUserDto>();
            CreateMap<ExtZoomUserDto, TbExtZoomUser>()
                .ForMember(dest => dest.TbHosts, opt => opt.MapFrom(src => (ICollection<TbHost>)null));

            // host summary
            CreateMap<TbUser, HostSummaryDto>()
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.Img.ThumbnailUrl));

            #endregion [User]

            #region [Img]

            CreateMap<TbImg, ImgDto>();
            CreateMap<ImgDto, TbImg>();

            #endregion [Img]

            #region [Mail]

            CreateMap<TbMail, MailDto>();
            CreateMap<MailDto, TbMail>();

            #endregion [Mail]

            #region [Meeting]

            CreateMap<TbMeetingCreationTracking, MeetingCreationTrackingDto>();
            CreateMap<MeetingCreationTrackingDto, TbMeetingCreationTracking>();

            CreateMap<TbMeetingUpdateTracking, MeetingUpdateTrackingDto>();
            CreateMap<MeetingUpdateTrackingDto, TbMeetingUpdateTracking>();

            CreateMap<TbMeetingRecordingDownloadTracking, MeetingRecordingDownloadTrackingDto>();
            CreateMap<MeetingRecordingDownloadTrackingDto, TbMeetingRecordingDownloadTracking>();

            CreateMap<TbMeetingStatus, MeetingStatusDto>();
            CreateMap<MeetingStatusDto, TbMeetingStatus>();

            CreateMap<TbTimezone, TimezoneDto>();
            CreateMap<TimezoneDto, TbTimezone>();

            CreateMap<TbMeetingReview, MeetingReviewDto>();
            CreateMap<MeetingReviewDto, TbMeetingReview>()
                .ForMember(dest => dest.MeetingUserMapping, opt => opt.MapFrom(src => (TbMeetingUserMapping)null));

            CreateMap<TbExtZoomMeeting, ExtZoomMeetingDto>();
            CreateMap<ExtZoomMeetingDto, TbExtZoomMeeting>();

            CreateMap<TbExtZoomMeetingRecording, ExtZoomMeetingRecordingDto>();
            CreateMap<ExtZoomMeetingRecordingDto, TbExtZoomMeetingRecording>();

            CreateMap<TbExtZoomWebHook, ExtZoomMeetingWebHookDto>();
            CreateMap<ExtZoomMeetingWebHookDto, TbExtZoomWebHook>();

            CreateMap<TbFocusMeetingMapping, FocusMeetingMappingDto>();
            CreateMap<FocusMeetingMappingDto, TbFocusMeetingMapping>()
                .ForMember(dest => dest.Focus, opt => opt.MapFrom(src => (TbFocus)null));

            CreateMap<TbFocusMeetingMapping, FocusMeetingMappingDto>();
            CreateMap<FocusMeetingMappingDto, TbFocusMeetingMapping>()
                .ForMember(dest => dest.Focus, opt => opt.MapFrom(src => (TbFocus)null));

            CreateMap<TbMeetingUserMapping, MeetingUserMappingDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Reviews, opt => opt.MapFrom(src => src.TbMeetingReviews));
            CreateMap<MeetingUserMappingDto, TbMeetingUserMapping>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => (TbUser)null))
                .ForMember(dest => dest.TbMeetingReviews, opt => opt.MapFrom(src => (ICollection<TbMeetingReview>)null));

            CreateMap<TbMeeting, MeetingDto>()
                .ForMember(dest => dest.ExtMeetingWebHooks, opt => opt.MapFrom(src => src.TbExtZoomWebHooks))
                .ForMember(dest => dest.Foci, opt => opt.MapFrom(src => src.TbFocusMeetingMappings))
                .ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.TbMeetingUserMappings))
                .ForMember(dest => dest.CreationUser, opt => opt.MapFrom(src => src.CreationUser))
                //.ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => (TimezoneDto)null)) //added - 9/4/2021
                .ForMember(dest => dest.Recordings, opt => opt.MapFrom(src => src.TbMeetingRecordings)) //added - 10/2/2021
                .ForMember(dest => dest.HasPassed, opt => opt.MapFrom(src => src.StartDateUtc <= System.DateTime.UtcNow)); //added - 16/09/2021
            CreateMap<MeetingDto, TbMeeting>()
                .ForMember(dest => dest.TbFocusMeetingMappings, opt => opt.MapFrom(src => src.Foci))
                .ForMember(dest => dest.MeetingStatus, opt => opt.MapFrom(src => (TbMeetingStatus)null))
                .ForMember(dest => dest.Host, opt => opt.MapFrom(src => (TbHost)null))
                .ForMember(dest => dest.TbExtZoomWebHooks, opt => opt.MapFrom(src => (ICollection<TbExtZoomWebHook>)null))
                .ForMember(dest => dest.TbMeetingUserMappings, opt => opt.MapFrom(src => src.Participants))
                .ForMember(dest => dest.CreationUser, opt => opt.MapFrom(src => (TbUser)null))
                .ForMember(dest => dest.Timezone, opt => opt.MapFrom(src => (TbTimezone)null)) //added - 9/4/2021
                //.ForMember(dest => dest.TbMeetingRecording, opt => opt.MapFrom(src => (ICollection<TbMeetingRecording>)null)); //added - 10/2/2021, commented out - 27/03/2021
                .ForMember(dest => dest.TbMeetingRecordings, opt => opt.MapFrom(src => src.Recordings)); //new - 27/3/2021

            CreateMap<TbMeetingRecording, MeetingRecordingDto>();
            CreateMap<MeetingRecordingDto, TbMeetingRecording>()
                .ForMember(dest => dest.Meeting, opt => opt.MapFrom(src => (TbMeeting)null));
            
            // meeting summary
            CreateMap<TbMeeting, MeetingSummaryDto>()
                .ForMember(dest => dest.CreationUserThumbnailUrl, opt => opt.MapFrom(src => src.CreationUser.Img.ThumbnailUrl))
                .ForMember(dest => dest.StatusDescription, opt => opt.MapFrom(src => src.MeetingStatus.Description))
                .ForMember(dest => dest.CreationUserName, opt => opt.MapFrom(src => src.CreationUser.Name))
                .ForMember(dest => dest.CreationUserSurname, opt => opt.MapFrom(src => src.CreationUser.Surname))
                .ForMember(dest => dest.CreationUserUsername, opt => opt.MapFrom(src => src.CreationUser.Username))
                .ForMember(dest => dest.CreationUserUniqueId, opt => opt.MapFrom(src => src.CreationUser.UniqueId))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.Img.ThumbnailUrl))
                .ForMember(dest => dest.HasPassed, opt => opt.MapFrom(src => src.StartDateUtc <= System.DateTime.UtcNow))
                .ForMember(dest => dest.HasRecordings, opt => opt.MapFrom(src => src.TbMeetingRecordings.Any()));

            #endregion [Meeting]

            #region [Notification]

            CreateMap<TbNotificationTracking, NotificationTrackingDto>();
            CreateMap<NotificationTrackingDto, TbNotificationTracking>();

            CreateMap<TbNotification, NotificationDto>();
            CreateMap<NotificationDto, TbNotification>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => (TbUser)null));

            #endregion [Notification]

            #region [Video]

            CreateMap<TbFocusVideoMapping, FocusVideoMappingDto>();
            CreateMap<FocusVideoMappingDto, TbFocusVideoMapping>()
                .ForMember(dest => dest.Focus, opt => opt.MapFrom(src => (TbFocus)null));

            CreateMap<TbVideo, VideoDto>()
                .ForMember(dest => dest.Foci, opt => opt.MapFrom(src => src.TbFocusVideoMappings));
            CreateMap<VideoDto, TbVideo>()
                .ForMember(dest => dest.TbFocusVideoMappings, opt => opt.MapFrom(src => src.Foci))
                .ForMember(dest => dest.CreationUser, opt => opt.MapFrom(src => (TbUser)null));

            // summary
            CreateMap<TbVideo, VideoSummaryDto>()
                .ForMember(dest => dest.CreationUserUniqueId, opt => opt.MapFrom(src => src.CreationUser.UniqueId))
                .ForMember(dest => dest.CreationUserThumbnailUrl, opt => opt.MapFrom(src => src.CreationUser.Img.ThumbnailUrl))
                .ForMember(dest => dest.CreationUserName, opt => opt.MapFrom(src => src.CreationUser.Name))
                .ForMember(dest => dest.CreationUserSurname, opt => opt.MapFrom(src => src.CreationUser.Surname))
                .ForMember(dest => dest.ThumbnailUrl, opt => opt.MapFrom(src => src.Img.ThumbnailUrl))
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Vid.Url));

            CreateMap<TbVid, VidDto>();
            CreateMap<VidDto, TbVid>();

            #endregion [Video]
        }
    }
}
