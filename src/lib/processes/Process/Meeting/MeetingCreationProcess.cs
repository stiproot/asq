using DTO.Events;
using DTO.Tracking;
using DTO.Zoom.Meeting;
using managers.Resource;
using processes.Engine;
using processes.Adapter;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace processes.Process.Meeting
{
    public class MeetingCreationProcess : IProcess
    {
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IAccountResourceManager _accountResourceManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IImgResourceManager _imgResourceManager;
        private ILogger _logger;

        public IEvent Event{ get; set; }
        private MeetingCreationEvent _event => (MeetingCreationEvent)Event;
        private readonly string _trackingComponentId = "meeting-creation";
        private MeetingCreationTrackingDto _tracker;
        private Guid _userGuid;


        public MeetingCreationProcess(
            IEnginePacketFactory packetFactory,
            ITaskRunnerFactory taskRunnerFactory,
            IStrategyFactoryFactory strategyFactoryFactory,
            IHandleExceptionStrategyFactoryFactory exceptionHandlerStrategyFactoryFactory,
            ITrackingAdapterFactory trackingAdapterFactory,
            IZoomResourceManager zoomResourceManager,
            IAccountResourceManager accountResourceManager,
            IUserResourceManager userResourceManager,
            IMeetingResourceManager meetingResourceManager,
            IImgResourceManager imgResourceManager
        )
        {
            this._packetFactory = packetFactory;
            this._taskRunnerFactory = taskRunnerFactory;
            this._strategyFactoryFactory = strategyFactoryFactory;
            this._exceptionHandlerFactoryFactory = exceptionHandlerStrategyFactoryFactory;
            this._trackingAdapterFactory = trackingAdapterFactory;
            this._zoomResourceManager = zoomResourceManager;
            this._accountResourceManager = accountResourceManager;
            this._userResourceManager = userResourceManager;
            this._meetingResourceManager = meetingResourceManager;
            this._imgResourceManager = imgResourceManager;
        }

        public IProcess SetLogger(ILogger logger = null)
        {
            _logger = logger;
            return this;
        }

        public IProcess SetEvent(IEvent @event)
        {
            Event = @event;
            return this;
        }

        public IProcess Init()
        {
            this.BuildTracker();
            return this;
        }

        private void BuildTracker()
        {
            try
            {
                var trackingComponents = new List<TrackingComponent>
                {
                    new TrackingComponent
                    {
                        identifier = this._trackingComponentId
                    }
                };

                this._tracker = new MeetingCreationTrackingDto
                {
                    UniqueId = Guid.NewGuid(),
                    CreationDateUtc = DateTime.UtcNow,
                    CreationUserId = 0,
                    Inactive = false,
                    Request = JsonSerializer.Serialize(this._event.meeting),
                    Failed = false,
                    Tracking = JsonSerializer.Serialize(trackingComponents)
                };

                this._event.tracking_id = this._tracker.UniqueId;
            }
            catch(Exception ex)
            {
                this._logger.LogError("Creation of meeting creation tracking entity failed | {0}", ex.StackTrace);
                throw;
            }
        }

        public async Task Execute()
        {
            // 0. persist tracking
            // 1. create zoom meeting
            // 2. write img
            // 3. persist meeting

            // validation
            if(this._event.meeting.Timezone == null) throw new InvalidOperationException($"Timezone not provided, meeting creation cannot continue");

            var user = await this._userResourceManager.GetHost(this._event.meeting.HostId);
            if(user == null) throw new InvalidOperationException($"User not found with host id ({this._event.meeting.HostId}) provided");

            if(this._tracker == null) throw new Exception($"Meeting creation tracking object does not exist, process cannot continue");

            await this._meetingResourceManager.AddMeetingCreationTracking(this._tracker);

            // Define primary Task
            var te = this._taskRunnerFactory.Create() 
                .AddParam(this._packetFactory.Create(this._event.meeting, "meeting"))
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create(
                        MeetingOperationEnu.CREATE,
                        this._packetFactory,
                        this._meetingResourceManager
                    )
                )
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id, 
                        this._trackingComponentId, 
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.CREATE, this._meetingResourceManager)
                    )
                );

            var extUser = user.Host.ExtUser.DeserializedPayload;
            var extMeetingReq = ExtMeetingCreationRequestFactory.CreateExtMeetingCreationRequestWithDefaults();
            extMeetingReq.start_time = this._event.meeting.StartDateUtc;
            extMeetingReq.topic = this._event.meeting.Title;
            extMeetingReq.agenda = this._event.meeting.Description;
            extMeetingReq.duration = this._event.meeting.EstimatedDuration;
            extMeetingReq.settings.contact_email = extUser.email;
            extMeetingReq.timezone = this._event.meeting.Timezone.ExtCode;

            var zoomStratTask = this._taskRunnerFactory.Create()
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create(
                        ZoomOperationEnu.CreateMeetingStrategy,
                        this._packetFactory, 
                        this._zoomResourceManager
                    ).SetNextParamName("extMeeting")
                )
                .AddParam(this._packetFactory.Create(extMeetingReq, "meetingConfig"))
                .AddParam(this._packetFactory.Create(extUser, "extUser"))
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id, 
                        this._trackingComponentId, 
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.CREATE, this._meetingResourceManager)
                    )
                );

            te.AddParam(zoomStratTask);

            var cancellationTokenSource = new CancellationTokenSource();
            await te.Run(cancellationTokenSource);
            cancellationTokenSource.Dispose();
        }
    }
}