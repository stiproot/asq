using System.Threading.Tasks;
using DTO.Tracking;

namespace dbaccess.Repository
{
    public interface IMeetingCreationTrackingResourceAccess
    {
        Task<MeetingCreationTrackingDto> GetMeetingCreationTracking(object id);
        Task<MeetingCreationTrackingDto> AddMeetingCreationTracking(MeetingCreationTrackingDto dto);
        Task UpdateMeetingCreationTracking(MeetingCreationTrackingDto dto);
    }
}