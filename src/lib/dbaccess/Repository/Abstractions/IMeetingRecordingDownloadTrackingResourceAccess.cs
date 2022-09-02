using System.Threading.Tasks;
using DTO.Tracking;

namespace dbaccess.Repository
{
    public interface IMeetingRecordingDownloadTrackingResourceAccess
    {
        Task<MeetingRecordingDownloadTrackingDto> GetMeetingRecordingDownloadTracking(object id);
        Task<MeetingRecordingDownloadTrackingDto> AddMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto);
        Task UpdateMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto);
    }
}