using System;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;
using DTO.Events;
using managers.Resource;
using processes.Process;
using processes.Factory;
using dbaccess.Factory.Test;

namespace processes.Tests
{
    public class MeetingUpdateProcessTests
    {
        private const bool _runUpdateMeetingProcess = false;
        private readonly ITestMeetingFactory _testMeetingFactory;
        private readonly IAccountResourceManager _accountManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;
        //private const string _zoomMeetingResponse = "{\"uuid\": \"Hc9UcDKlTc+s7mjjwGxiNw==\",\"id\": 99695203079,\"host_id\": \"6A42KiD5SoGVA2z-ru9jSw\",\"host_email\": \"simon@asq.properties.co.za\",\"topic\": \"test-topic\",\"type\": 2,\"status\": \"waiting\",\"start_time\": \"2020-10-26T17:00:00Z\",\"duration\": 30,\"timezone\": \"Africa/Harare\",\"agenda\": \"test-agenda\",\"created_at\": \"2020-10-26T11:47:54Z\",\"start_url\": \"https://zoom.us/s/99695203079?zak=eyJ6bV9za20iOiJ6bV9vMm0iLCJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJjbGllbnQiLCJ1aWQiOiI2QTQyS2lENVNvR1ZBMnotcnU5alN3IiwiaXNzIjoid2ViIiwic3R5Ijo5OSwid2NkIjoiYXcxIiwiY2x0IjowLCJzdGsiOiJCVG53aTdWNVVGNl9scWN3QkdSSGE1VDhYNFd0RENUSHJpdDJETlJzVjFvLkJnVXNhV05yZUVKbFltMXdURW94WkcweGJrVnlTMWRxZVdoT1pFTmtVblJFWlc4NGRqVXJiWGh4U25SaGR6MEFBQXd6UTBKQmRXOXBXVk16Y3owQUEyRjNNUSIsImV4cCI6MTYxMTQ4ODg3NCwiaWF0IjoxNjAzNzEyODc0LCJhaWQiOiJCOEZTeVhlVFJiYUpIY2xFUkJXQmR3IiwiY2lkIjoiIn0.oK6XMKLO5krq6znnvddvFKxvUDS6OBNo_jKYqVSHhkk\",\"join_url\": \"https://zoom.us/j/99695203079?pwd=TzRvamhVVTQ5N25WTWw2ZG4zRG4vdz09\",\"password\": \"test\",\"h323_password\": \"374313\",\"pstn_password\": \"374313\",\"encrypted_password\": \"TzRvamhVVTQ5N25WTWw2ZG4zRG4vdz09\",\"settings\": {\"host_video\": true,\"participant_video\": true,\"cn_meeting\": false,\"in_meeting\": false,\"join_before_host\": true,\"mute_upon_entry\": true,\"watermark\": false,\"use_pmi\": false,\"approval_type\": 2,\"audio\": \"both\",\"auto_recording\": \"none\",\"enforce_login\": false,\"enforce_login_domains\": \"\",\"alternative_hosts\": \"\",\"close_registration\": false,\"show_share_button\": false,\"allow_multiple_devices\": false,\"registrants_confirmation_email\": true,\"waiting_room\": true,\"request_permission_to_unmute_participants\": false,\"global_dial_in_countries\": [\"US\"],\"global_dial_in_numbers\": [{\"country_name\": \"US\",\"number\": \"+1 3017158592\",\"type\": \"toll\",\"country\": \"US\"}],\"registrants_email_notification\": false,\"meeting_authentication\": false,\"encryption_type\": \"enhanced_encryption\"}}";

        public MeetingUpdateProcessTests(
            ITestMeetingFactory meetingFactory,
            IAccountResourceManager accountManager,
            IMeetingResourceManager meetingResourceManager,
            IProcessFactory processFactory,
            ITestOutputHelper output,
            IMapper mapper)
        {
            this._testMeetingFactory = meetingFactory;
            this._accountManager = accountManager;
            this._meetingResourceManager = meetingResourceManager;
            this._processFactory = processFactory;
            this._output = output;
            this._mapper = mapper;
        }

        [Fact]
        public void UpdateMeetingTest()
        {
            try
            {
                if(!_runUpdateMeetingProcess) return;

                Task t = UpdateMeetingProcess();
                t.Wait();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateMeetingProcess()
        {
            Guid meetingUid = Guid.Parse("0d177281-1d85-4230-8d46-326ca590ed9f");

            // arrange
            var meeting = await this._meetingResourceManager.GetMeeting(meetingUid);
            Assert.NotNull(meeting);

            // update meeting
            meeting.StartDateUtc = meeting.StartDateUtc.AddDays(3);
            meeting.EstimatedDuration = meeting.EstimatedDuration + 30;
            meeting.Title = meeting.Title + " - modified";
            meeting.Description = meeting.Description + " - modified";

            // create update meeting process event
            var @event = new MeetingUpdateEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                meeting = meeting,
            };

            // create process
            var process = _processFactory.Create(ProcessTypeEnu.UpdateMeetingProcess)
                                    .SetEvent(@event)
                                    //.SetLogger(_logger)
                                    .Init();
            // act
            await process.Execute();
        }
    }
}
