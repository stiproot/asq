using DTO.Events;
using DTO.Tracking;
using managers.Resource;
using processes.Engine;
using processes.Adapter;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace processes.Process.Meeting
{
    public class MeetingRecordingDownloadProcess : IProcess
    {
        private ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly ISharedContextFactory _sharedContextFactory;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;

        public IEvent Event{ get; set; }
        private MeetingRecordingDownloadEvent _event => (MeetingRecordingDownloadEvent)Event;
        private readonly string _trackingComponentId = "meeting-recording-download";
        private MeetingRecordingDownloadTrackingDto _tracker;
        private CancellationTokenSource CancellationTokenSourceFactory() => new CancellationTokenSource();

        public MeetingRecordingDownloadProcess(
            IConfiguration configuration,
            IEnginePacketFactory packetFactory,
            ITaskRunnerFactory taskRunnerFactory,
            IStrategyFactoryFactory strategyFactoryFactory,
            IHandleExceptionStrategyFactoryFactory exceptionHandlerStrategyFactoryFactory,
            ITrackingAdapterFactory trackingAdapterFactory,
            ISharedContextFactory sharedContextFactory,
            IZoomResourceManager zoomResourceManager,
            IMeetingResourceManager meetingResourceManager
        )
        {
            this._configuration = configuration;
            this._packetFactory = packetFactory;
            this._taskRunnerFactory = taskRunnerFactory;
            this._strategyFactoryFactory = strategyFactoryFactory;
            this._exceptionHandlerFactoryFactory = exceptionHandlerStrategyFactoryFactory;
            this._trackingAdapterFactory = trackingAdapterFactory;
            this._sharedContextFactory = sharedContextFactory;
            this._zoomResourceManager = zoomResourceManager;
            this._meetingResourceManager = meetingResourceManager;
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

                this._tracker = new MeetingRecordingDownloadTrackingDto
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
                this._logger.LogError("Create meeting tracking failed - {0}", ex.StackTrace);
                throw;
            }
        }

        public async Task Execute()
        {
            this._logger.LogInformation("Meeting recording download process starting...");

            // add tracking object
            this._logger.LogInformation("Attempting meeting recording download tracking persist");
            await this._meetingResourceManager.AddMeetingRecordingDownloadTracking(this._tracker);

            // create context
            var context = this._sharedContextFactory.Create();

            // add meeting from event to context
            var meetingKey = Guid.NewGuid();
            context.AddResult(meetingKey, this._event.meeting);

            // add ext meeting id from event to context
            var extMeetingIdKey = Guid.NewGuid();
            context.AddResult(extMeetingIdKey, this._event.meeting.ExtMeeting.Id);

            // add recording file path to context
            var recordingDirPathKey = Guid.NewGuid();
            var recordingDirPath = $"{this._configuration["StaticFileServerSettings:VideoBasePath"]}{this._event.meeting.UniqueId}/";
            this._logger.LogInformation("Recording dir path {0}", recordingDirPath);
            context.AddResult(recordingDirPathKey, recordingDirPath);

            // add static video server url to context
            var staticVideoServerUrlKey = Guid.NewGuid();
            var staticVideoServerUrl = $"{this._configuration["StaticFileServerSettings:StaticVideoServerBaseUrl"]}{this._event.meeting.UniqueId}/";
            this._logger.LogInformation("Static video server base url {0}", staticVideoServerUrl);
            context.AddResult(staticVideoServerUrlKey, staticVideoServerUrl);

            // get meeting recording from zoom api
            var getZoomMeetingRecordingNode = this._taskRunnerFactory.Create()
                .SetSharedContext(context)
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create(
                        ZoomOperationEnu.GetMeetingRecordingStrategy,
                        this._packetFactory,
                        this._zoomResourceManager
                    ))
                .AddParam(this._packetFactory.Create(this._event.meeting, "meeting"))
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id,
                        this._trackingComponentId,
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.DOWNLOAD_RECORDING, this._meetingResourceManager)
                    ));

            // persist external meeting recording entry
            var persistExtMeetingRecordingNode = this._taskRunnerFactory.Create()
                .SetSharedContext(context)
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create(
                        MeetingOperationEnu.CREATE_EXT_MEETING_RECORDING,
                        this._packetFactory,
                        this._meetingResourceManager
                    ))
                .AddParam(
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(getZoomMeetingRecordingNode.Id), "getZoomMeetingRecordingResponse"),
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(extMeetingIdKey), "extMeetingId")
                )
                .AddParam(getZoomMeetingRecordingNode)
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id,
                        this._trackingComponentId,
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.DOWNLOAD_RECORDING, this._meetingResourceManager)
                    ));

            // download media files
            var downloadRecordingsNode = this._taskRunnerFactory.Create()
                .SetSharedContext(context)
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create(
                        MeetingOperationEnu.DOWNLOAD_RECORDING,
                        this._packetFactory,
                        this._zoomResourceManager
                    ))
                .AddParam(
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(persistExtMeetingRecordingNode.Id), "extMeetingRecording"),
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(recordingDirPathKey), "recordingDirPath")
                )
                .AddParam(persistExtMeetingRecordingNode)
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id,
                        this._trackingComponentId,
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.DOWNLOAD_RECORDING, this._meetingResourceManager)
                    ));

            // read folder & persist meeting recording entries
            var updateMeetingStatusRoot = this._taskRunnerFactory.Create()
                .SetSharedContext(context)
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create(
                        MeetingOperationEnu.SCAN_MEETING_RECORDING_DIR_AND_UPDATE_MEETING_STATUS,
                        this._packetFactory,
                        this._meetingResourceManager
                    ))
                .AddParam(
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(persistExtMeetingRecordingNode.Id), "extMeetingRecording"),
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(recordingDirPathKey), "recordingDirPath"),
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(staticVideoServerUrlKey), "staticVideoServerUrl"),
                    (ISharedContext context) => this._packetFactory.Create(context.GetResult(meetingKey), "meeting")
                )
                .AddParam(downloadRecordingsNode)
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id,
                        this._trackingComponentId,
                        this._trackingAdapterFactory.Create(MeetingOperationEnu.DOWNLOAD_RECORDING, this._meetingResourceManager)
                    ));

            var cancelationTokenSource = this.CancellationTokenSourceFactory();
            await updateMeetingStatusRoot.Run(cancelationTokenSource);
            cancelationTokenSource.Dispose();
        }
    }
}