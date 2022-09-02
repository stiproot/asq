using System.Threading.Tasks;
using DTO.Domain.Ext.Zoom;

namespace dbaccess.Repository
{
    public interface IExtZoomMeetingRecordingResourceAccess
    {
        Task<ExtZoomMeetingRecordingDto> GetExtZoomMeetingRecording(object id);
        Task<ExtZoomMeetingRecordingDto> AddExtZoomMeetingRecording(ExtZoomMeetingRecordingDto dto);
    }
}