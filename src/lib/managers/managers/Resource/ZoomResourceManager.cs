using System.Collections.Generic;
using System.Threading.Tasks;
using ZoomClient;
using ZoomClient.Factory;
using DTO.Zoom.User;
using DTO.Zoom.Meeting;

namespace managers.Resource
{
    public class ZoomResourceManager: IZoomResourceManager
    {
        private readonly IZoomHttpClient _zoomHttpClient;
        private readonly ISignatureBuilderFactory _signatureBuilderFactory;
        private readonly IJwtBuilderFactory _jwtBuilderFactory;

        public ZoomResourceManager(
            IZoomHttpClient zoomHttpClient, 
            ISignatureBuilderFactory signatureBuilderFactory,
            IJwtBuilderFactory jwtBuilderFactory)
        {
            this._zoomHttpClient = zoomHttpClient;
            this._signatureBuilderFactory = signatureBuilderFactory;
            this._jwtBuilderFactory = jwtBuilderFactory;
        }

        public async Task<GetZoomUser> GetUser(string userId)
            => await this._zoomHttpClient.GetUser(userId);

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
            => await this._zoomHttpClient.CreateUser(request);

        public async Task DeleteUser(string extId)
            => await this._zoomHttpClient.DeleteUser(extId);

        public async Task<IEnumerable<GetListMeetingResponse>> GetListMeetings(string userId)
            => await this._zoomHttpClient.GetListMeetings(userId);

        public async Task<GetMeetingResponse> GetMeeting(string meetingId)
            => await this._zoomHttpClient.GetMeeting(meetingId);

        public async Task<GetMeetingRecordingsResponse> GetMeetingRecordings(string meetingId)
            => await this._zoomHttpClient.GetMeetingRecordings(meetingId);

        public async Task<CreateMeetingResponse> CreateMeeting(string userId, CreateMeetingRequest request)
            => await this._zoomHttpClient.CreateMeeting(userId, request);

        public async Task UpdateMeeting(long meetingId, UpdateMeetingRequest request)
            => await this._zoomHttpClient.UpdateMeeting(meetingId, request);

        public string GetSignature(string role, string meetingId)
            => this._signatureBuilderFactory
                .Create()
                .SetRole(role)
                .SetMeetingNo(meetingId)
                .Build();

        public string GetJwt() => this._jwtBuilderFactory.Create().SetExpiration(System.DateTime.Now.AddMinutes(15)).Build();
    }
}