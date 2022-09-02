using DTO.Events;
using DTO.Tracking;
using DTO.Notification;
using DTO.Domain;
using managers.Resource;
using MailClient.Api;
using MailClient.Providers;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace processes.Process.Notification
{
    public class QueueNotificationProcess : IProcess
    {
        private ILogger _logger;
        private readonly INotificationResourceManager _notificationResourceManager;
        private readonly IMailClientApi _mailClientApi;

        public IEvent Event{ get; set; }
        private SendNotificationEvent _event => (SendNotificationEvent)Event;
        private readonly string _trackingComponentId = "mail-creation";

        private NotificationTrackingDto _tracker;
        private BaseTracking _mailTracker;
        private NotificationDto _notification;

        public QueueNotificationProcess(
            INotificationResourceManager notificationResourceManager,
            IMailClientApi mailClientApi
        )
        {
            this._notificationResourceManager = notificationResourceManager ?? throw new ArgumentNullException(nameof(notificationResourceManager));
            this._mailClientApi = mailClientApi ?? throw new ArgumentNullException(nameof(mailClientApi));
        }

        public IProcess SetLogger(ILogger logger = null)
        {
            _logger = logger;
            return this;
        }

        public IProcess SetEvent(IEvent @event)
        {
            Event = @event;
            return this;
        }

        public IProcess Init()
        {
            this.BuildTracker();
            return this;
        }

        private void BuildTracker()
        {
            try
            {
                var trackingComponents = new List<TrackingComponent>
                {
                    new TrackingComponent
                    {
                        identifier = this._trackingComponentId
                    }
                };

                this._tracker = new NotificationTrackingDto
                {
                    UniqueId = Guid.NewGuid(),
                    CreationDateUtc = DateTime.UtcNow,
                    CreationUserId = 0,
                    Inactive = false,
                    Request = JsonSerializer.Serialize(this._event.notification_config),
                    Failed = false,
                    Tracking = JsonSerializer.Serialize(trackingComponents)
                };
            }
            catch(Exception ex)
            {
                this._logger.LogError($"create tracking failed {ex.StackTrace}");
                throw;
            }
        }

        public async Task Execute()
        {
            // 0. Persist tracker
            await this._notificationResourceManager.AddNotificationTracking(this._tracker);

            var mail = this.MapMailsFromNotificationConfiguration(this._event.notification_config);
            var notifications = this.MapNotificationsFromNotificationConfiguration(this._event.notification_config);

            var tasks = new List<Task>();

            if(mail.Any())
            {
                tasks.Add
                (
                    Task.Run(() => 
                    {
                        try
                        {
                            this._notificationResourceManager.AddMail(mail);
                        }
                        catch(Exception ex)
                        {
                            this._logger.LogError(ex, "Logged from queue notification process. AddMail.");
                            throw;
                        }
                    })
                );
            }
            if(notifications.Any())
            {
                tasks.Add
                (
                    Task.Run(() => 
                    {
                        try
                        {
                            this._notificationResourceManager.AddNotifications(notifications);
                        }
                        catch(Exception ex)
                        {
                            this._logger.LogError(ex, "Logged from queue notification process. AddNotifications.");
                            throw;
                        }
                    })
                );
            }

            var task = Task.WhenAll(tasks);
            await task;
        }

        private IEnumerable<NotificationDto> MapNotificationsFromNotificationConfiguration(NotificationConfig notificationConfig)
        {
            var notifications = new List<NotificationDto>();

            if
            (
                notificationConfig.NotificationType == NotificationTypeEnu.MEETING_REGISTRATION
            )
            {
                string message = $"{notificationConfig.ParticipantName} '{notificationConfig.ParticipantUsername}' {notificationConfig.ParticipantSurname} has just signed up for your meeting, {notificationConfig.MeetingTitle}";
                var notification = new NotificationDto()
                {
                    Title = "New Meeting Participant",
                    Message = message, 
                    UserId = notificationConfig.UserId,
                    Seen = false,
                    ImgUrl = notificationConfig.MeetingThumbnailUrl,
                    MeetingUrl = notificationConfig.MeetingUrl,
                    ExtMeetingUrl = notificationConfig.ExtMeetingStartUrl,
                    NotificationTypeId = (short)NotificationTypeEnu.MEETING_REGISTRATION
                };

                notifications.Add(notification);
            }

            return notifications;
        }

        private IEnumerable<MailDto> MapMailsFromNotificationConfiguration(NotificationConfig notificationConfig)
        {
            var mails = new List<MailDto>();

            if
            (
                notificationConfig.NotificationType == NotificationTypeEnu.MEETING_CREATION ||
                notificationConfig.NotificationType == NotificationTypeEnu.EMAIL_CONFIRMATION ||
                notificationConfig.NotificationType == NotificationTypeEnu.MEETING_REGISTRATION ||
                notificationConfig.NotificationType == NotificationTypeEnu.WELCOME
            )
            {
                var noReplyFromEmailAddress = this._mailClientApi.MailSettingProvider.GetFromMailAddress();
                var subject = MailSubjectProvider.GetSubject(notificationConfig.NotificationType);
                var htmlBody = this._mailClientApi.MailTemplateProvider.GetTemplateContents(notificationConfig.NotificationType)
                    .Replace("{{name}}", notificationConfig.ToName)
                    .Replace("{{surname}}", notificationConfig.ToSurname)
                    .Replace("{{username}}", notificationConfig.ToUsername);
                
                if(notificationConfig.NotificationType == NotificationTypeEnu.MEETING_REGISTRATION)
                {
                    htmlBody = htmlBody 
                        .Replace("{{meeting-title}}", notificationConfig.MeetingTitle)
                        .Replace("{{participant-name}}", notificationConfig.ParticipantName)
                        .Replace("{{participant-surname}}", notificationConfig.ParticipantSurname)
                        .Replace("{{participant-username}}", notificationConfig.ParticipantUsername);
                }
                else if(notificationConfig.NotificationType == NotificationTypeEnu.EMAIL_CONFIRMATION)
                {
                    string url = $"{this._mailClientApi.MailSettingProvider.GetEmailConfirmationUrl()}{notificationConfig.ToUniqueId}";
                    htmlBody = htmlBody.Replace("{{email-confirmation-url}}", url);
                }
                else if(notificationConfig.NotificationType == NotificationTypeEnu.MEETING_CREATION)
                {
                    string url = $"{this._mailClientApi.MailSettingProvider.GetMeetingGatewayUrl()}{notificationConfig.MeetingId}";
                    htmlBody = htmlBody
                        .Replace("{{meeting-url}}", notificationConfig.ExtMeetingStartUrl)
                        .Replace("{{ext-meeting-url}}", url)
                        .Replace("{{meeting_timezone}}", notificationConfig.MeetingTimezone)
                        .Replace("{{meeting_date}}", notificationConfig.MeetingStartDatetime);
                }

                var mail = new MailDto()
                {
                    Subject = subject, 
                    Body = htmlBody, 
                    ToEmailAddress = notificationConfig.ToEmailAddress,
                    FromEmailAddress = noReplyFromEmailAddress,
                    StatusId = (short)MailTrackingStatusEnu.AWAITING
                };

                mails.Add(mail);
            }

            return mails;
        }
    }
}