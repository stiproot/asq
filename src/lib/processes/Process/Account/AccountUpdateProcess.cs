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
using System.Linq;
using System;

namespace processes.Process.Account
{
    public class AccountUpdateProcess : IProcess
    {
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IAccountResourceManager _accountResourceManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IImgResourceManager _imgResourceManager;
        private ILogger _logger;

        public IEvent Event{ get; set; }
        private AccountUpdateEvent _event => (AccountUpdateEvent)Event;
        private readonly string _trackingComponentId = "account-update";
        private AccountUpdateTrackingDto _tracker;


        public AccountUpdateProcess(
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

            this._tracker = new AccountUpdateTrackingDto
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

        public async Task Execute()
        {
            // 0. persist tracking

            // what could have chnaged?
            // 1) user information... do we need to update Zoom account info... I think not
            // 2) account type, delete zoom account
            // 3) image... write new image
            // if downgrade, we remove ext user, change account type & remove host id

            // get original user entry to compare...
            var originalUser = await this._userResourceManager.GetUser(this._event.user.Id);

            // validation
            // what types of validation do we want to perform?
            // 1. make sure username, name or surname was not changed
            if(this._event.user.Name != originalUser.Name)
            {
                throw new InvalidOperationException("Name cannot be modified");
            }
            if(this._event.user.Surname != originalUser.Surname)
            {
                throw new InvalidOperationException("Surname cannot be modified");
            }
            if(this._event.user.Username != originalUser.Username)
            {
                throw new InvalidOperationException("Username cannot be modified");
            }
            if(this._event.user.Email != originalUser.Email)
            {
                throw new InvalidOperationException("Email cannot be modified");
            }
            if(!this._event.user.Interests.Any())
            {
                throw new InvalidOperationException("Interests cannot be empty");
            }
            if(this._event.user.Host !=null && !this._event.user.Host.Specialities.Any())
            {
                throw new InvalidOperationException("Host specialities cannot be empty");
            }

            // what type of update are we performing?
            bool isUpgrade = this._event.user.AccountType == AccountTypeEnu.HOST && originalUser.AccountType == AccountTypeEnu.STUDENT;
            bool isDowngrade = this._event.user.AccountType == AccountTypeEnu.STUDENT && originalUser.AccountType == AccountTypeEnu.HOST;

            // have interests or specialities been removed?
            bool hasInterestsBeenRemoved = this._event.user.Interests.Count() < originalUser.Interests.Count();
            bool hasSpecialitiesBeenRemoved = 
                !isUpgrade && 
                !isDowngrade && 
                this._event.user.AccountType == AccountTypeEnu.HOST && 
                this._event.user.Host.Specialities.Count() < originalUser.Host.Specialities.Count();

            ITaskRunner te = this._taskRunnerFactory.Create()
                .SetExceptionHandler(
                    this._exceptionHandlerFactoryFactory.CreateFactory(
                        this._event.tracking_id, 
                        this._trackingComponentId, 
                        this._trackingAdapterFactory.Create(AccountOperationEnu.EDIT, this._accountResourceManager)
                    )
                )
                .AddParam(this._packetFactory.Create(this._event.user, "user"))
                .SetStrategyFactory(this._strategyFactoryFactory.Create(
                    AccountOperationEnu.EDIT,
                    this._packetFactory,
                    this._userResourceManager
                ));

            // "upgrading" student to host
            if(isUpgrade)
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

                ITaskRunner zoomStratTask = this._taskRunnerFactory.Create()
                    .SetStrategyFactory(
                        this._strategyFactoryFactory.Create(
                            ZoomOperationEnu.CreateUserStrategy,
                            this._packetFactory, 
                            this._zoomResourceManager
                        ).SetNextParamName("extUser")
                    )
                    .AddParam(this._packetFactory.Create(extUserReq, "hostConfig"))
                    .SetExceptionHandler(
                        this._exceptionHandlerFactoryFactory.CreateFactory(
                            this._event.tracking_id, 
                            this._trackingComponentId, 
                            this._trackingAdapterFactory.Create(AccountOperationEnu.EDIT, this._accountResourceManager)
                        )
                    );

                te.AddParam(zoomStratTask);
            }
            // "downgrading" host to student
            else if(isDowngrade)
            {
                ITaskRunner zoomDeleteUserStratTask = this._taskRunnerFactory.Create()
                    .SetStrategyFactory(
                        this._strategyFactoryFactory.Create(
                            ZoomOperationEnu.DeleteUserStrategy,
                            this._packetFactory, 
                            this._zoomResourceManager
                        )
                    )
                    .AddParam(this._packetFactory.Create(originalUser.Host.ExtUser.DeserializedPayload.id, "extUserId"))
                    .SetExceptionHandler(
                        this._exceptionHandlerFactoryFactory.CreateFactory(
                            this._event.tracking_id, 
                            this._trackingComponentId, 
                            this._trackingAdapterFactory.Create(AccountOperationEnu.EDIT, this._accountResourceManager)
                        )
                    );

                te.AddParam(zoomDeleteUserStratTask);
            }

            if(hasInterestsBeenRemoved)
            {
                var mappingsToRemove = originalUser.Interests.Where(oi => !this._event.user.Interests.Any(i => oi.FocusId.Equals(i.FocusId)));
                var removeInterestsTask = this._taskRunnerFactory.Create()
                    .SetExceptionHandler(
                        this._exceptionHandlerFactoryFactory.CreateFactory
                        (
                            this._event.tracking_id, 
                            this._trackingComponentId, 
                            this._trackingAdapterFactory.Create(AccountOperationEnu.EDIT, this._accountResourceManager)
                        )
                    )
                    .SetStrategyFactory(async(IDictionary<string, IEnginePacket> parameters) => 
                    {
                        await this._userResourceManager.DeleteFocusUserMapping(mappingsToRemove);
                        return null;
                    });
                
                te.AddParam(removeInterestsTask);
            }

            if(hasSpecialitiesBeenRemoved)
            {
                var mappingsToRemove = originalUser.Host.Specialities.Where(os => !this._event.user.Host.Specialities.Any(s => os.FocusId.Equals(s.FocusId)));
                var removeSpecialitiesTask = this._taskRunnerFactory.Create()
                    .SetExceptionHandler(
                        this._exceptionHandlerFactoryFactory.CreateFactory
                        (
                            this._event.tracking_id, 
                            this._trackingComponentId, 
                            this._trackingAdapterFactory.Create(AccountOperationEnu.EDIT, this._accountResourceManager)
                        )
                    )
                    .SetStrategyFactory(async(IDictionary<string, IEnginePacket> parameters) => 
                    {
                        await this._userResourceManager.DeleteFocusHostMapping(mappingsToRemove);
                        return null;
                    });
                
                te.AddParam(removeSpecialitiesTask);
            }

            // persist tracking
            await this._accountResourceManager.AddAccountUpdateTracking(this._tracker);

            // run process
            var cancelationTokenSource = new CancellationTokenSource();
            await te.Run(cancelationTokenSource);
            cancelationTokenSource.Dispose();
        }
    }
}