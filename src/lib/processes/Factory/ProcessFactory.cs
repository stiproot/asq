using processes.Process;
using processes.Process.Account;
using processes.Process.Meeting;
using processes.Process.Mail;
using processes.Process.Notification;
using processes.Engine;
using processes.Adapter;
using managers.Resource;
using MailClient.Api;
using Microsoft.Extensions.Configuration;
using System;

namespace processes.Factory
{
    public class ProcessFactory : IProcessFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly ISharedContextFactory _sharedContextFactory;
        private readonly IZoomResourceManager _zoomResourceManager;
        private readonly IAccountResourceManager _accountResourceManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IMeetingResourceManager _meetingResourceManager;
        private readonly IImgResourceManager _imgResourceManager;
        private readonly INotificationResourceManager _notificationResourceManager;
        private readonly IMailClientApi _mailClientApi;

        public ProcessFactory(
            IConfiguration configuration,
            IEnginePacketFactory packetFactory,
            ITaskRunnerFactory taskRunnerFactory,
            IStrategyFactoryFactory strategyFactoryFactory,
            IHandleExceptionStrategyFactoryFactory exceptionHandlerFactoryFactory,
            ITrackingAdapterFactory trackingAdapterFactory,
            ISharedContextFactory sharedContextFactory,
            IZoomResourceManager zoomResourceManager,
            IAccountResourceManager accountResourceManager,
            IUserResourceManager userResourceManager,
            IMeetingResourceManager meetingResourceManager,
            IImgResourceManager imgResourceManager,
            INotificationResourceManager notificationResourceManager,
            IMailClientApi mailClientApi
        )
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._packetFactory = packetFactory ?? throw new ArgumentNullException(nameof(packetFactory));
            this._taskRunnerFactory = taskRunnerFactory ?? throw new ArgumentNullException(nameof(taskRunnerFactory));
            this._strategyFactoryFactory = strategyFactoryFactory ?? throw new ArgumentNullException(nameof(strategyFactoryFactory));
            this._exceptionHandlerFactoryFactory = exceptionHandlerFactoryFactory ?? throw new ArgumentNullException(nameof(exceptionHandlerFactoryFactory));
            this._trackingAdapterFactory = trackingAdapterFactory ?? throw new ArgumentNullException(nameof(trackingAdapterFactory));
            this._sharedContextFactory = sharedContextFactory ?? throw new ArgumentNullException(nameof(sharedContextFactory));
            this._zoomResourceManager = zoomResourceManager ?? throw new ArgumentNullException(nameof(zoomResourceManager));
            this._accountResourceManager = accountResourceManager ?? throw new ArgumentNullException(nameof(accountResourceManager));
            this._userResourceManager = userResourceManager ?? throw new ArgumentNullException(nameof(userResourceManager));
            this._meetingResourceManager = meetingResourceManager ?? throw new ArgumentNullException(nameof(meetingResourceManager));
            this._imgResourceManager = imgResourceManager ?? throw new ArgumentNullException(nameof(imgResourceManager));
            this._notificationResourceManager = notificationResourceManager ?? throw new ArgumentNullException(nameof(notificationResourceManager));
            this._mailClientApi = mailClientApi ?? throw new ArgumentNullException(nameof(mailClientApi));
        }

        public IProcess Create(ProcessTypeEnu type) =>
            type switch
            {
                ProcessTypeEnu.CreateAccountProcess =>
                    new AccountCreationProcess(
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._zoomResourceManager,
                        this._accountResourceManager,
                        this._userResourceManager,
                        this._imgResourceManager),

                ProcessTypeEnu.UpdateAccountProcess => 
                    new AccountUpdateProcess(
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._zoomResourceManager,
                        this._accountResourceManager,
                        this._userResourceManager,
                        this._imgResourceManager),

                ProcessTypeEnu.CreateMeetingProcess=> 
                    new MeetingCreationProcess(
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._zoomResourceManager,
                        this._accountResourceManager,
                        this._userResourceManager,
                        this._meetingResourceManager,
                        this._imgResourceManager),

                ProcessTypeEnu.UpdateMeetingProcess =>
                    new MeetingUpdateProcess(
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._zoomResourceManager,
                        this._accountResourceManager,
                        this._userResourceManager,
                        this._meetingResourceManager,
                        this._imgResourceManager),

                ProcessTypeEnu.ParticipateInMeetingProcess => 
                    new MeetingParticipationProcess(
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._accountResourceManager,
                        this._meetingResourceManager
                    ),

                ProcessTypeEnu.SendMailProcess =>
                    new SendMailProcess(
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._notificationResourceManager
                    ),

                ProcessTypeEnu.QueueNotificationProcess => 
                    new QueueNotificationProcess(
                        this._notificationResourceManager,
                        this._mailClientApi
                    ),

                ProcessTypeEnu.DownloadMeetingRecordingProcess => 
                    new MeetingRecordingDownloadProcess(
                        this._configuration,
                        this._packetFactory,
                        this._taskRunnerFactory,
                        this._strategyFactoryFactory,
                        this._exceptionHandlerFactoryFactory,
                        this._trackingAdapterFactory,
                        this._sharedContextFactory,
                        this._zoomResourceManager,
                        this._meetingResourceManager
                    ),

                _ => throw new NotImplementedException()
        };
    }
}