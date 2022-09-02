using DTO.Events;
using managers.Resource;
using processes.Engine;
using processes.Adapter;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace processes.Process.Meeting
{
    public class MeetingParticipationProcess : IProcess
    {
        private ILogger _logger;
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly IAccountResourceManager _accountResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;

        public IEvent Event{ get; set; }
        private MeetingParticipationEvent _event => (MeetingParticipationEvent)Event;

        public MeetingParticipationProcess(
            IEnginePacketFactory packetFactory,
            ITaskRunnerFactory taskRunnerFactory,
            IStrategyFactoryFactory strategyFactoryFactory,
            IHandleExceptionStrategyFactoryFactory exceptionHandlerStrategyFactoryFactory,
            ITrackingAdapterFactory trackingAdapterFactory,
            IAccountResourceManager accountResourceManager,
            IMeetingResourceManager meetingResourceManager
        )
        {
            this._packetFactory = packetFactory;
            this._taskRunnerFactory = taskRunnerFactory;
            this._strategyFactoryFactory = strategyFactoryFactory;
            this._exceptionHandlerFactoryFactory = exceptionHandlerStrategyFactoryFactory;
            this._trackingAdapterFactory = trackingAdapterFactory;
            this._accountResourceManager = accountResourceManager;
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
            return this;
        }

        public async Task Execute()
        {
            // some validation will be handled on a Task level as data first needs to be retrieved

            // define primary Task to be update meeting
            ITaskRunner te = this._taskRunnerFactory.Create() 
                .SetStrategyFactory(this._strategyFactoryFactory.Create(
                    MeetingOperationEnu.PARTICIPATE,
                    this._packetFactory,
                    this._meetingResourceManager
                ))
                .AddParam
                (
                    this._packetFactory.Create(this._event.user_id, "userId"),
                    this._packetFactory.Create(this._event.register, "register")
                );
            
            if(this._event.meeting != null)
            {
                // the meeting, for whatever reason has been provided, perhaps it was already in the request context
                // so let's use it...
                te.AddParam(this._packetFactory.Create(this._event.meeting, "meeting"));
            }
            else
            {
                // otherwise let's create a task to get it
                ITaskRunner getMeetingTask = this._taskRunnerFactory.Create()
                    .SetStrategyFactory(
                        this._strategyFactoryFactory.Create(
                            MeetingOperationEnu.GET,
                            this._packetFactory, 
                            this._meetingResourceManager
                        ).SetNextParamName("meeting")
                    )
                    .AddParam(this._packetFactory.Create(this._event.meeting_id, "meetingId"));

                te.AddParam(getMeetingTask);
            }

            var cancellationTokenSource = new CancellationTokenSource();
            await te.Run(cancellationTokenSource);
            cancellationTokenSource.Dispose();
        }
    }
}