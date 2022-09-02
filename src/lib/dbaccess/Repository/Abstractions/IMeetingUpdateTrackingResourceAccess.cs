using System.Threading.Tasks;
using DTO.Tracking;

namespace dbaccess.Repository
{
    public interface IMeetingUpdateTrackingResourceAccess
    {
        Task<MeetingUpdateTrackingDto> GetMeetingUpdateTracking(object id);
        Task<MeetingUpdateTrackingDto> AddMeetingUpdateTracking(MeetingUpdateTrackingDto dto);
        Task UpdateMeetingUpdateTracking(MeetingUpdateTrackingDto dto);
    }
}