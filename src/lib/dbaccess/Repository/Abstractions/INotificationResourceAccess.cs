using DTO.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dbaccess.Repository
{
    public interface INotificationResourceAccess
    {
        Task<NotificationDto> GetNotification(object id);
        Task<NotificationDto> AddNotification(NotificationDto dto);
        Task AddNotifications(IEnumerable<NotificationDto> dtos);
        Task UpdateNotification(NotificationDto dto);
        Task UpdateNotification(IEnumerable<NotificationDto> dtos);
        Task<IEnumerable<NotificationDto>> GetUnseenNotifications(NotificationQueryDto query);
        Task ReadNotification(object id);
    }
}