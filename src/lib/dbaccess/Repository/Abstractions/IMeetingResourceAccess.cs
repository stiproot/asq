using DTO.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dbaccess.Repository
{
    public interface IMeetingResourceAccess
    {
        Task<MeetingDto> GetMeeting(object id);
        Task<IEnumerable<MeetingSummaryDto>> GetMeetingSummariesByFilter(MeetingFilterConfigDto filter);
        Task<MeetingDto> AddMeeting(MeetingDto dto);
        Task UpdateMeeting(MeetingDto dto);
        Task UpdateMeetings(IEnumerable<MeetingDto> dto);
        Task DeleteMeeting(object id);
        Task<MeetingDto> GetMeetingByExtMeetingId(long extMeetingId);
        Task<long> GetMeetingIdByExtMeetingId(long extMeetingId);
        Task UpdateMeetingStatus(object id);
        Task<IEnumerable<MeetingDto>> GetCompleteMeetingsAndMarkAsRecordingDownloading();
    }
}