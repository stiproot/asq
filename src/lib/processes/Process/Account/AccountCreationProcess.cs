using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using DTO.Events;
using DTO.Tracking;
using DTO.Domain;
using DTO.Zoom.User;
using managers.Resource;
using processes.Engine;
using processes.Adapter;
using System;

namespace processes.Process.Account
{
    public class AccountCreationProcess : IProcess
    {
        private ILogger _logger;
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IAccountResourceManager _accountResourceManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IImgResourceManager _imgResourceManager;

        private readonly string _trackingComponentId = "account-creation";
        public IEvent Event{ get; set; }
        private AccountCreationEvent _event => (AccountCreationEvent)Event;
        private AccountCreationTrackingDto _tracker;

        public AccountCreationProcess(
            IEnginePacketFactory packetFactory,
            ITaskRunnerFactory taskRunnerFactory,
            IStrategyFactoryFactory strategyFactoryFactory,
            IHandleExceptionStrategyFactoryFactory exceptionHandlerStrategyFactoryFactory,
            ITrackingAdapterFactory trackingAdapterFactory,
            IZoomResourceManager zoomResourceManager,
            IAccountResourceManager accountResourceManager,
            IUserResourceManager userResourceManager,
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
            this._event.user.Inactive = true;
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

                this._tracker = new AccountCreationTrackingDto
                {
                    UniqueId = Guid.NewGuid(),
                    CreationDateUtc = DateTime.UtcNow,
                    CreationUserId = 0,
                    Inactive = false,
                    Request = JsonSerializer.Serialize(this._event.user),
                    Failed = false,
                    Tracking = JsonSerializer.Serialize(trackingComponents)
                };

                this._event.tracking_id = this._tracker.UniqueId;
            }
            catch(Exception ex)
            {
                this._logger.LogError($"Tracking creation failed {ex.StackTrace}");
                throw;
            }
        }

        public async Task Execute()
        {
            // 0. persist tracking
            // 1. create zoom account (only if host)
            // 2. write profile img
            // 3. persist user (root)

            await this._accountResourceManager.AddAccountCreationTracking(this._tracker);
            
            var userPacket = this._packetFactory.Create(this._event.user, "user");

            var primaryTaskExceptionHandler = this._exceptionHandlerFactoryFactory.CreateFactory
            (
                this._event.tracking_id, 
                this._trackingComponentId, 
                this._trackingAdapterFactory.Create(AccountOperationEnu.CREATE, this._accountResourceManager)
            );

            Func<Exception, Task> primaryExceptionWrapper = async (Exception ex) =>
            {
                this._logger.LogError(ex, "User creation node failed.");
                await primaryTaskExceptionHandler(ex);
            };

            if(primaryTaskExceptionHandler == null) throw new OperationCanceledException("Primary task exception handler is null");

            var root = this._taskRunnerFactory.Create()
                .SetExceptionHandler
                (
                    primaryExceptionWrapper
                    //primaryTaskExceptionHandler
                )
                .AddParam(userPacket)
                .SetStrategyFactory(
                    this._strategyFactoryFactory.Create
                    (
                        AccountOperationEnu.CREATE,
                        this._packetFactory,
                        this._userResourceManager
                    ));

            if(this._event.user.AccountType == AccountTypeEnu.HOST)
            {
                var extUserReq = new CreateUserRequest
                {
                    action = "custCreate",
                    user_info = new UserInfo
                    {
                        type = UserTypeEnu.Basic,
                        first_name = _event.user.Name,
                        last_name = _event.user.Surname,
                        email = $"{_event.user.Username}@asq.properties",
                        password = null
                    }
                };

                Func<Exception, Task> zoomExceptionHandler = async (ex) =>
                {
                    this._logger.LogError(ex, "Zoom account creaton node failed");

                    var handler = this._exceptionHandlerFactoryFactory.CreateFactory
                    (
                        this._event.tracking_id, 
                        this._trackingComponentId, 
                        this._trackingAdapterFactory.Create(AccountOperationEnu.CREATE, this._accountResourceManager)
                    );
                    await handler(ex);
                };

                var zoomNode = this._taskRunnerFactory.Create()
                  .SetStrategyFactory(
                      this._strategyFactoryFactory.Create(
                          ZoomOperationEnu.CreateUserStrategy,
                          this._packetFactory, 
                          this._zoomResourceManager
                      )
                      .SetNextParamName("extUser")
                  )
                  .AddParam(this._packetFactory.Create(extUserReq, "hostConfig"))
                  .SetExceptionHandler(
                      zoomExceptionHandler
                      //this._exceptionHandlerFactoryFactory.CreateFactory(
                          //this._event.tracking_id, 
                          //this._trackingComponentId, 
                          //this._trackingAdapterFactory.Create(AccountOperationEnu.CREATE, this._accountResourceManager)
                      //)
                  );

                root.AddParam(zoomNode);
            }

            var tokenSource = new CancellationTokenSource();
            await root.Run(tokenSource);
            tokenSource.Dispose();
        }
    }
}
