using DTO.Zoom.User;
using DTO.Zoom.Meeting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZoomClient
{
    public interface IZoomHttpClient
    {
        Task<GetZoomUser> GetUser(string userId);
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        Task DeleteUser(string userId);

        Task<IEnumerable<GetListMeetingResponse>> GetListMeetings(string userId);
        Task<GetMeetingResponse> GetMeeting(string meetingId);
        Task<GetMeetingRecordingsResponse> GetMeetingRecordings(string meetingId);
        Task<CreateMeetingResponse> CreateMeeting(string userId, CreateMeetingRequest request);
        Task UpdateMeeting(long meetingId, UpdateMeetingRequest request);
        Task DeleteMeeting(long meetingId);
    }
}