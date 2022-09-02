using DTO.Events;
using DTO.Tracking;
using managers.Resource;
using processes.Engine;
using processes.Adapter;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace processes.Process.Mail
{
    public class SendMailProcess : IProcess
    {
        private ILogger _logger;
        private readonly IEnginePacketFactory _packetFactory;
        private readonly ITaskRunnerFactory _taskRunnerFactory;
        private readonly IStrategyFactoryFactory _strategyFactoryFactory;
        private readonly IHandleExceptionStrategyFactoryFactory _exceptionHandlerFactoryFactory;
        private readonly ITrackingAdapterFactory _trackingAdapterFactory;
        private readonly INotificationResourceManager _notificationResourceManger;

        public IEvent Event{ get; set; }
        private SendMailEvent _event => (SendMailEvent)Event;

        public SendMailProcess(
            IEnginePacketFactory packetFactory,
            ITaskRunnerFactory taskRunnerFactory,
            IStrategyFactoryFactory strategyFactoryFactory,
            IHandleExceptionStrategyFactoryFactory exceptionHandlerStrategyFactoryFactory,
            ITrackingAdapterFactory trackingAdapterFactory,
            INotificationResourceManager notificationResourceManager
        )
        {
            this._packetFactory = packetFactory ?? throw new ArgumentNullException(nameof(packetFactory));
            this._taskRunnerFactory = taskRunnerFactory ?? throw new ArgumentNullException(nameof(taskRunnerFactory));
            this._strategyFactoryFactory = strategyFactoryFactory ?? throw new ArgumentNullException(nameof(strategyFactoryFactory));
            this._exceptionHandlerFactoryFactory = exceptionHandlerStrategyFactoryFactory ?? throw new ArgumentNullException(nameof(exceptionHandlerStrategyFactoryFactory));
            this._trackingAdapterFactory = trackingAdapterFactory ?? throw new ArgumentNullException(nameof(trackingAdapterFactory));
            this._notificationResourceManger = notificationResourceManager ?? throw new ArgumentNullException(nameof(notificationResourceManager));
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
            // validation
            if(this._event.mails_to_send == null || !this._event.mails_to_send.Any()) throw new InvalidOperationException("Send mail process aborting, notification config is null");

            var tasks = this._event.mails_to_send.Select(m =>
            {
                return this._taskRunnerFactory.Create()
                    .SetStrategyFactory
                    (
                        async (IDictionary<string, IEnginePacket> parameters) => 
                        {
                            await this._notificationResourceManger.SendMail(m);
                            m.StatusId = (short)MailTrackingStatusEnu.SENT;
                            await this._notificationResourceManger.UpdateMail(m);

                            return null;
                        }
                    )
                    .AddParam(this._packetFactory.Create(m, "mail"))
                    .SetExceptionHandler(
                        async (Exception ex) => 
                        {
                            var mail = await this._notificationResourceManger.GetMail(m.UniqueId);
                            mail.StatusId = (short)MailTrackingStatusEnu.FAILED;
                            await this._notificationResourceManger.UpdateMail(mail);
                        }
                    )
                    .Run(null);
            });

            await Task.WhenAll(tasks).ConfigureAwait(false);

            foreach(var mail in this._event.mails_to_send)
            {
                mail.StatusId = (short)DTO.Tracking.MailTrackingStatusEnu.SENT;
            }
            await this._notificationResourceManger.UpdateMail(this._event.mails_to_send).ConfigureAwait(false);
        }
    }
}