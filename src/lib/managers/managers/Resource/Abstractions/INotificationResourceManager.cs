using DTO.Domain;
using DTO.Tracking;
using DTO.Notification;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace managers.Resource
{
    public interface INotificationResourceManager
    {
        // Notification tracking
        Task<NotificationTrackingDto> GetNotificationTracking(object id) ;
        Task AddNotificationTracking(NotificationTrackingDto dto);
        Task UpdateNotificationTracking(NotificationTrackingDto dto);

        // Notification
        Task<NotificationDto> GetNotification(object id);
        Task AddNotification(NotificationDto dto);
        Task AddNotifications(IEnumerable<NotificationDto> dtos);
        Task UpdateNotification(NotificationDto dto);
        Task UpdateNotification(IEnumerable<NotificationDto> dtos);
        Task<IEnumerable<NotificationDto>> GetUnseenNotifications(NotificationQueryDto query);
        Task ReadNotification(object id);

        // Mail
        Task<MailDto> GetMail(Guid id);
        Task AddMail(MailDto dto);
        Task AddMail(IEnumerable<MailDto> dtos);
        Task UpdateMail(MailDto dto);
        Task UpdateMail(IEnumerable<MailDto> dtos);
        Task SendMail(MailDto dto);
        Task<IEnumerable<MailDto>> GetPendingMailAndMarkForProcessing();
    }
}