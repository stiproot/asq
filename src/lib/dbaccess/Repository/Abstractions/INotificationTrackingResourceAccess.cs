using System.Threading.Tasks;
using DTO.Tracking;

namespace dbaccess.Repository
{
    public interface INotificationTrackingResourceAccess
    {
        Task<NotificationTrackingDto> GetNotificationTracking(object id);
        Task<NotificationTrackingDto> AddNotificationTracking(NotificationTrackingDto dto);
        Task UpdateNotificationTracking(NotificationTrackingDto dto);
    }
}