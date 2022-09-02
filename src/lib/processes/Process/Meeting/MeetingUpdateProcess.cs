using DTO.Events;
using DTO.Tracking;
using DTO.Zoom.Meeting;
using managers.Resource;
using processes.Engine;
using processes.Adapter;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace processes.Process.Meeting
{
    public class MeetingUpdateProcess : IProcess
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
        private MeetingUpdateEvent _event => (MeetingUpdateEvent)Event;
        private readonly string _trackingComponentId = "meeting-update";
        private MeetingUpdateTrackingDto _tracker;
        private CancellationTokenSource CancellationTokenSourceFactory() => new CancellationTokenSource();


        public MeetingUpdateProcess(
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
            var trackingComponents = new List<TrackingComponent>
            {
                new TrackingComponent
                {
                    identifier = this._trackingComponentId
                }
            };

            this._tracker = new MeetingUpdateTrackingDto
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

        public async Task Execute()
        {
            // 0. persist tracking
            // 1. update zoom meeting (dependent)
            // 1. write new image (dependent)
            // 2. update meeting

            // validation
            if(this._event.meeting.Timezone == null)
            {
                throw new Exception($"Timezone not provided, meeting creation cannot continue");
            }

            // user id is required, should it not be passed in via a builder method?
            var user = await this._userResourceManager.GetHost(this._event.meeting.HostId);
            if(user == null)
            {
                throw new Exception($"User not found with host id ({this._event.meeting.HostId}) provided");
            }

            // original meeting is required to compare against new meeting.
            // we would also like to update the orignal creation response payload.
            var originalMeeting = await this._meetingResourceManager.GetMeeting(this._event.meeting.UniqueId);
            if(originalMeeting == null)
            {
                throw new Exception($"Original meeting not found with meeting id ({this._event.meeting.Id}) provided");
            }

            if(this._tracker == null)
            {
                throw new Exception($"Meeting update tracking object does not exist, process cannot continue");
            }

            // let's assume the meeting has been changed, if it has not... well then no harm done.
            // we are updating the original meeting creation resonse payload here.
            var originalExtMeetingResp = originalMeeting.ExtMeeting.PayloadSerializer;
            originalExtMeetingResp.start_time = this._event.meeting.StartDateUtc;
            originalExtMeetingResp.duration = this._event.meeting.EstimatedDuration;
            originalExtMeetingResp.topic = this._event.meeting.Title;
            originalExtMeetingResp.agenda = this._event.meeting.Description;
            originalExtMeetingResp.timezone = this._event.meeting.Timezone.ExtCode;
            this._event.meeting.ExtMeeting.PayloadSerializer = originalExtMeetingResp;

            //throw new Exception(JsonSerializer.Serialize(this._event.meeting));

            // add meeting update tracking object
            await this._meetingResourceManager.AddMeetingUpdateTracking(this._tracker);

            // define primary Task to be update meeting
            ITaskRunner te = this._taskRunnerFactory.Create() 
                .SetStrategyFactory(this._strategyFactoryFactory.Create(
                    MeetingOperationEnu.EDIT,
                    this._packetFactory,
                    this._meetingResourceManager
                ))
                .AddParam(this._packetFactory.Create(this._event.meeting, "meeting"))
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id, 
                        this._trackingComponentId, 
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.EDIT, this._meetingResourceManager)
                    )
                );
            
            // let's determine if we need to update the zoom meeting. ie. maybe only the img changed
            if(originalMeeting.isDiff(this._event.meeting))
            {
                var extMeetingUpdateReq = ExtMeetingCreationRequestFactory.CreateExtMeetingUpdateRequestFromOriginalResponse(originalMeeting.ExtMeeting.PayloadSerializer);
                extMeetingUpdateReq.start_time = this._event.meeting.StartDateUtc;
                extMeetingUpdateReq.topic = this._event.meeting.Title;
                extMeetingUpdateReq.agenda = this._event.meeting.Description;
                extMeetingUpdateReq.duration = this._event.meeting.EstimatedDuration;
                extMeetingUpdateReq.timezone = this._event.meeting.Timezone.ExtCode;

                ITaskRunner zoomStratTask = this._taskRunnerFactory.Create()
                    .SetStrategyFactory(
                        this._strategyFactoryFactory.Create(
                            ZoomOperationEnu.UpdateMeetingStrategy,
                            this._packetFactory, 
                            this._zoomResourceManager
                        )
                    )
                    .AddParam(this._packetFactory.Create(extMeetingUpdateReq, "updateMeetingConfig"))
                    .AddParam(this._packetFactory.Create(originalMeeting.ExtMeeting.PayloadSerializer.id, "extMeetingId"))
                    .SetExceptionHandler(
                        this._exceptionHandlerFactoryFactory.CreateFactory(
                            this._event.tracking_id, 
                            this._trackingComponentId, 
                            this._trackingAdapterFactory.Create(MeetingOperationEnu.EDIT, this._meetingResourceManager)
                        )
                    );

                te.AddParam(zoomStratTask);
            }

            var cancellationTokenSource = this.CancellationTokenSourceFactory();
            await te.Run(cancellationTokenSource);
            cancellationTokenSource.Dispose();
        }
    }
}