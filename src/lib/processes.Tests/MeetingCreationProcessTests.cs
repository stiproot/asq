using DTO.Zoom.Meeting;
using DTO.Events;
using managers.Resource;
using processes.Process;
using processes.Factory;
using dbaccess.Factory.Test;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Xunit;
using Xunit.Abstractions;

namespace processes.Tests
{
    public class MeetingCreationProcessTests
    {
        private const bool _runMeetingCreationProcess = false;
        private readonly ITestMeetingFactory _testMeetingFactory;
        private readonly IAccountResourceManager _accountManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly ITestOutputHelper _output;
        private readonly IMapper _mapper;
        private const string _zoomMeetingResponse = "{\"uuid\": \"Hc9UcDKlTc+s7mjjwGxiNw==\",\"id\": 99695203079,\"host_id\": \"6A42KiD5SoGVA2z-ru9jSw\",\"host_email\": \"simon@asq.properties.co.za\",\"topic\": \"test-topic\",\"type\": 2,\"status\": \"waiting\",\"start_time\": \"2020-10-26T17:00:00Z\",\"duration\": 30,\"timezone\": \"Africa/Harare\",\"agenda\": \"test-agenda\",\"created_at\": \"2020-10-26T11:47:54Z\",\"start_url\": \"https://zoom.us/s/99695203079?zak=eyJ6bV9za20iOiJ6bV9vMm0iLCJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJjbGllbnQiLCJ1aWQiOiI2QTQyS2lENVNvR1ZBMnotcnU5alN3IiwiaXNzIjoid2ViIiwic3R5Ijo5OSwid2NkIjoiYXcxIiwiY2x0IjowLCJzdGsiOiJCVG53aTdWNVVGNl9scWN3QkdSSGE1VDhYNFd0RENUSHJpdDJETlJzVjFvLkJnVXNhV05yZUVKbFltMXdURW94WkcweGJrVnlTMWRxZVdoT1pFTmtVblJFWlc4NGRqVXJiWGh4U25SaGR6MEFBQXd6UTBKQmRXOXBXVk16Y3owQUEyRjNNUSIsImV4cCI6MTYxMTQ4ODg3NCwiaWF0IjoxNjAzNzEyODc0LCJhaWQiOiJCOEZTeVhlVFJiYUpIY2xFUkJXQmR3IiwiY2lkIjoiIn0.oK6XMKLO5krq6znnvddvFKxvUDS6OBNo_jKYqVSHhkk\",\"join_url\": \"https://zoom.us/j/99695203079?pwd=TzRvamhVVTQ5N25WTWw2ZG4zRG4vdz09\",\"password\": \"test\",\"h323_password\": \"374313\",\"pstn_password\": \"374313\",\"encrypted_password\": \"TzRvamhVVTQ5N25WTWw2ZG4zRG4vdz09\",\"settings\": {\"host_video\": true,\"participant_video\": true,\"cn_meeting\": false,\"in_meeting\": false,\"join_before_host\": true,\"mute_upon_entry\": true,\"watermark\": false,\"use_pmi\": false,\"approval_type\": 2,\"audio\": \"both\",\"auto_recording\": \"none\",\"enforce_login\": false,\"enforce_login_domains\": \"\",\"alternative_hosts\": \"\",\"close_registration\": false,\"show_share_button\": false,\"allow_multiple_devices\": false,\"registrants_confirmation_email\": true,\"waiting_room\": true,\"request_permission_to_unmute_participants\": false,\"global_dial_in_countries\": [\"US\"],\"global_dial_in_numbers\": [{\"country_name\": \"US\",\"number\": \"+1 3017158592\",\"type\": \"toll\",\"country\": \"US\"}],\"registrants_email_notification\": false,\"meeting_authentication\": false,\"encryption_type\": \"enhanced_encryption\"}}";

        public MeetingCreationProcessTests(
            ITestMeetingFactory meetingFactory,
            IAccountResourceManager accountManager,
            IMeetingResourceManager meetingResourceManager,
            IUserResourceManager userResourceManager,
            IProcessFactory processFactory,
            ITestOutputHelper output,
            IMapper mapper)
        {
            this._testMeetingFactory = meetingFactory;
            this._accountManager = accountManager;
            this._userResourceManager = userResourceManager;
            this._meetingResourceManager = meetingResourceManager;
            this._processFactory = processFactory;
            this._output = output;
            this._mapper = mapper;
        }


        //[Fact]
        //public void TestDataIsReturningValidData()
        //{
            //var meeting = this._testMeetingFactory.GenerateMeetingDto();

            //Assert.NotNull(meeting);
            //Assert.NotNull(meeting.Img);
            //Assert.NotNull(meeting.Foci);
            //Assert.Equal(meeting.Foci.Count(), 2);
            //Assert.True(meeting.HostId > 0);
        //}

        //[Fact]
        //public void TestMapperIsWorkingAsExpected()
        //{
            //var meeting = this._testMeetingFactory.GenerateMeetingDto();

            //meeting.Img.Path = "path 1";
            //meeting.Img.ThumbnailUrl = "path 2";
            //meeting.ExtMeeting = new DTO.Domain.Ext.Zoom.BaseExtZoomMeetingDto()
            //{
                //Payload = JsonSerializer.Serialize(JsonSerializer.Deserialize<CreateMeetingResponse>(_zoomMeetingResponse)),
                //MeetingId = 0
            //};

            //var ent = this._mapper.Map<MeetingDto, TbMeeting>(meeting);

            ////throw new Exception(JsonSerializer.Serialize(ent));

            //Assert.NotNull(meeting);
            //Assert.NotNull(meeting.Img);
            //Assert.NotNull(meeting.Foci);
            //Assert.Equal(meeting.Foci.Count(), 2);
            //Assert.True(meeting.HostId > 0);
        //}

        //[Fact]
        //public async void TestRetrievalOfHost()
        //{
            //var meeting = this._testMeetingFactory.GenerateMeetingDto();
            //var user = await this._userResourceManager.GetHost(meeting.HostId);

            //Assert.NotNull(user);
            //Assert.NotNull(user.Host);
            //Assert.NotNull(user.PaymentInfo);
            //Assert.NotNull(user.Host.ExtUser);
            //Assert.True(user.Interests.Count() > 0);
            //Assert.True(user.Host.Specialities.Count() > 0);
        //}

        [Fact]
        public void TestDeserializationOfSampleResponse()
        {
            var createMeetingResponse = JsonSerializer.Deserialize<CreateMeetingResponse>(_zoomMeetingResponse);
            Assert.NotNull(createMeetingResponse);
        }

        [Fact]
        public void TestBaseDtoIsDefaulting()
        {
            var createMeetingDto = new DTO.Domain.Ext.Zoom.ExtZoomMeetingDto()
            {
                Payload = "",
                //MeetingId = 0
            };

            Assert.NotNull(createMeetingDto.CreationDateUtc);
            Assert.NotNull(createMeetingDto.UniqueId);
        }

        [Fact]
        public void CreateMeetingTest()
        {
            try
            {
                if(!_runMeetingCreationProcess) return;

                Task t = RunMeetingCreationProcess();
                t.Wait();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task RunMeetingCreationProcess()
        {
            // arrange
            var meeting = this._testMeetingFactory.GenerateMeetingDto();
            var @event = new MeetingCreationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                meeting = meeting,
            };

            var process = _processFactory.Create(ProcessTypeEnu.CreateMeetingProcess)
                                    .SetEvent(@event)
                                    //.SetLogger(_logger)
                                    .Init();

            // act
            await process.Execute();
        }
    }
}
