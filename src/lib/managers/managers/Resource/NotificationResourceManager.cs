using DTO.Domain;
using DTO.Tracking;
using dbaccess.Repository;
using MailClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace managers.Resource
{
    public class NotificationResourceManager : INotificationResourceManager
    {
        private readonly INotificationResourceAccess _notificationResourceAccess;
        private readonly INotificationTrackingResourceAccess _notificationTrackingResourceAccess;
        private readonly IMailResourceAccess _mailResourceAccess;
        private readonly ISmtpMailClient _mailClient;

        public NotificationResourceManager(
            INotificationResourceAccess notificationResourceAccess,
            INotificationTrackingResourceAccess notificationTrackingResourceAccess,
            IMailResourceAccess mailResourceAccess,
            ISmtpMailClient mailClient
        )
        {
            this._notificationResourceAccess = notificationResourceAccess ?? throw new ArgumentNullException(nameof(notificationResourceAccess));
            this._notificationTrackingResourceAccess = notificationTrackingResourceAccess?? throw new ArgumentNullException(nameof(notificationTrackingResourceAccess));
            this._mailResourceAccess = mailResourceAccess ?? throw new ArgumentNullException(nameof(mailResourceAccess));
            this._mailClient = mailClient;
        }

        // Notification tracking
        public async Task<NotificationTrackingDto> GetNotificationTracking(object id) 
            => await this._notificationTrackingResourceAccess.GetNotificationTracking(id);
        public async Task AddNotificationTracking(NotificationTrackingDto dto)
            => await this._notificationTrackingResourceAccess.AddNotificationTracking(dto);
        public async Task UpdateNotificationTracking(NotificationTrackingDto dto)
            => await this._notificationTrackingResourceAccess.UpdateNotificationTracking(dto);

        // Notification
        public async Task<NotificationDto> GetNotification(object id) 
            => await this._notificationResourceAccess.GetNotification(id);
        public async Task AddNotification(NotificationDto dto)
            => await this._notificationResourceAccess.AddNotification(dto);
        public async Task AddNotifications(IEnumerable<NotificationDto> dtos)
            => await this._notificationResourceAccess.AddNotifications(dtos);
        public async Task UpdateNotification(NotificationDto dto)
            => await this._notificationResourceAccess.UpdateNotification(dto);
        public async Task UpdateNotification(IEnumerable<NotificationDto> dtos)
            => await this._notificationResourceAccess.UpdateNotification(dtos);
        public async Task<IEnumerable<NotificationDto>> GetUnseenNotifications(NotificationQueryDto query)
            => await this._notificationResourceAccess.GetUnseenNotifications(query);
        public async Task ReadNotification(object id)
            => await this._notificationResourceAccess.ReadNotification(id);

        // Mail
        public async Task AddMail(MailDto dto)
        {
            if(dto == null) throw new ArgumentNullException(nameof(dto));
            await this._mailResourceAccess.AddMail(dto);
        }
        public async Task AddMail(IEnumerable<MailDto> dtos)
        {
            if(dtos == null) throw new ArgumentNullException(nameof(dtos));
            await this._mailResourceAccess.AddMail(dtos);
        }
        public async Task UpdateMail(MailDto dto)
            => await this._mailResourceAccess.UpdateMail(dto);
        public async Task UpdateMail(IEnumerable<MailDto> dtos)
            => await this._mailResourceAccess.UpdateMail(dtos);
        public async Task<MailDto> GetMail(Guid id) 
            => await this._mailResourceAccess.GetMail(id);
        public async Task SendMail(MailDto dto) 
            => await this._mailClient.Send(dto);
        public async Task<IEnumerable<MailDto>> GetPendingMailAndMarkForProcessing()
            => await this._mailResourceAccess.GetPendingMailAndMarkForProcessing();
    }
}