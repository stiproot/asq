using DTO.Zoom.User;
using DTO.Zoom.Meeting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace managers.Resource
{
    public interface IZoomResourceManager
    {
        Task<GetZoomUser> GetUser(string userId);
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
        Task DeleteUser(string extId);

        Task<IEnumerable<GetListMeetingResponse>> GetListMeetings(string userId);
        Task<GetMeetingResponse> GetMeeting(string meetingId);
        Task<GetMeetingRecordingsResponse> GetMeetingRecordings(string meetingId);
        Task<CreateMeetingResponse> CreateMeeting(string userId, CreateMeetingRequest request);
        Task UpdateMeeting(long meetingId, UpdateMeetingRequest request);
        string GetSignature(string role, string meetingId);
        string GetJwt();
    }
}