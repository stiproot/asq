using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Zoom.Meeting;
using DTO.Zoom.User;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class CreateZoomMeetingStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingConfigParamName = "meetingConfig";
        private const string _extUserParamName = "extUser";
        private readonly IZoomResourceManager _resourceManager;

        public CreateZoomMeetingStrategyFactory(
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        ): base(packetFactory) => this._resourceManager = zoomResourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var extUser = param[_extUserParamName].Cast<CreateUserResponse>();
                var meetingConfig = param[_meetingConfigParamName].Cast<CreateMeetingRequest>();
                var resp = await this._resourceManager.CreateMeeting(extUser.id, meetingConfig);
                //string testRespStr = await Task.FromResult<string>("{\"uuid\": \"Hc9UcDKlTc+s7mjjwGxiNw==\",\"id\": 99695203079,\"host_id\": \"6A42KiD5SoGVA2z-ru9jSw\",\"host_email\": \"simon@asq.properties.co.za\",\"topic\": \"test-topic\",\"type\": 2,\"status\": \"waiting\",\"start_time\": \"2020-10-26T17:00:00Z\",\"duration\": 30,\"timezone\": \"Africa/Harare\",\"agenda\": \"test-agenda\",\"created_at\": \"2020-10-26T11:47:54Z\",\"start_url\": \"https://zoom.us/s/99695203079?zak=eyJ6bV9za20iOiJ6bV9vMm0iLCJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJjbGllbnQiLCJ1aWQiOiI2QTQyS2lENVNvR1ZBMnotcnU5alN3IiwiaXNzIjoid2ViIiwic3R5Ijo5OSwid2NkIjoiYXcxIiwiY2x0IjowLCJzdGsiOiJCVG53aTdWNVVGNl9scWN3QkdSSGE1VDhYNFd0RENUSHJpdDJETlJzVjFvLkJnVXNhV05yZUVKbFltMXdURW94WkcweGJrVnlTMWRxZVdoT1pFTmtVblJFWlc4NGRqVXJiWGh4U25SaGR6MEFBQXd6UTBKQmRXOXBXVk16Y3owQUEyRjNNUSIsImV4cCI6MTYxMTQ4ODg3NCwiaWF0IjoxNjAzNzEyODc0LCJhaWQiOiJCOEZTeVhlVFJiYUpIY2xFUkJXQmR3IiwiY2lkIjoiIn0.oK6XMKLO5krq6znnvddvFKxvUDS6OBNo_jKYqVSHhkk\",\"join_url\": \"https://zoom.us/j/99695203079?pwd=TzRvamhVVTQ5N25WTWw2ZG4zRG4vdz09\",\"password\": \"test\",\"h323_password\": \"374313\",\"pstn_password\": \"374313\",\"encrypted_password\": \"TzRvamhVVTQ5N25WTWw2ZG4zRG4vdz09\",\"settings\": {\"host_video\": true,\"participant_video\": true,\"cn_meeting\": false,\"in_meeting\": false,\"join_before_host\": true,\"mute_upon_entry\": true,\"watermark\": false,\"use_pmi\": false,\"approval_type\": 2,\"audio\": \"both\",\"auto_recording\": \"none\",\"enforce_login\": false,\"enforce_login_domains\": \"\",\"alternative_hosts\": \"\",\"close_registration\": false,\"show_share_button\": false,\"allow_multiple_devices\": false,\"registrants_confirmation_email\": true,\"waiting_room\": true,\"request_permission_to_unmute_participants\": false,\"global_dial_in_countries\": [\"US\"],\"global_dial_in_numbers\": [{\"country_name\": \"US\",\"number\": \"+1 3017158592\",\"type\": \"toll\",\"country\": \"US\"}],\"registrants_email_notification\": false,\"meeting_authentication\": false,\"encryption_type\": \"enhanced_encryption\"}}");
                //var resp = JsonSerializer.Deserialize<CreateMeetingResponse>(testRespStr);
                return this._packetFactory.Create(resp, this._nextParamName);
            };
        }
    }
}