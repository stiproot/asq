using DTO.Zoom.User;
using DTO.Zoom.Meeting;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ZoomClient
{
    public class ZoomHttpClient : IZoomHttpClient
    {
        private readonly IConfiguration _config;
        private readonly IGenericHttpClient _client;
        private string _baseUrl => _config["Zoom:WebApi:BaseUrl"];

        public ZoomHttpClient(IConfiguration config, IGenericHttpClient client)
        {
            _config = config;
            _client = client;
        }

        private string PrependBaseUrl(string uri)
        {
            return $"{_baseUrl}{uri}";
        }

        public async Task<GetZoomUser> GetUser(string userId)
        {
            string uri = $"users/{userId}";

            return await _client.Get<GetZoomUser>(PrependBaseUrl(uri));
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            string uri = "users";

            return await _client.Post<CreateUserResponse>(PrependBaseUrl(uri), request);
        }

        public async Task DeleteUser(string userId)
        {
            string uri = $"users/{userId}";

            await _client.Delete(PrependBaseUrl(uri));
        }


        public async Task<IEnumerable<GetListMeetingResponse>> GetListMeetings(string userId)
        {
            string uri = $"users/{userId}/meetings";

            return await _client.Get<IEnumerable<GetListMeetingResponse>>(PrependBaseUrl(uri));
        }

        public async Task<GetMeetingResponse> GetMeeting(string meetingId)
        {
            string uri = $"meetings/{meetingId}";

            return await _client.Get<GetMeetingResponse>(PrependBaseUrl(uri));
        }

        public async Task<GetMeetingRecordingsResponse> GetMeetingRecordings(string meetingId)
        {
            string uri = $"meetings/{meetingId}/recordings";

            return await _client.Get<GetMeetingRecordingsResponse>(PrependBaseUrl(uri));
        }

        public async Task<CreateMeetingResponse> CreateMeeting(string userId, CreateMeetingRequest request)
        {
            string uri = $"users/{userId}/meetings";

            return await _client.Post<CreateMeetingResponse>(PrependBaseUrl(uri), request);
        }

        public async Task UpdateMeeting(long meetingId, UpdateMeetingRequest request)
        {
            string uri = $"meetings/{meetingId}";

            await _client.Patch(PrependBaseUrl(uri), request);
        }

        public async Task DeleteMeeting(long meetingId)
        {
            string uri = $"meetings/{meetingId}";

            await _client.Delete(PrependBaseUrl(uri));
        }
    }
}