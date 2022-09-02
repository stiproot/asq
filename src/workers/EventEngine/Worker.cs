using processes.Factory;
using processes.Process;
using DTO.Events;
using managers.Resource;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace EventEngine
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IProcessFactory _processFactory;
        private readonly INotificationResourceManager _notificationResourceManager;

        public Worker(
            IConfiguration configuration, 
            ILogger<Worker> logger, 
            IProcessFactory processFactory,
            INotificationResourceManager notificationResourceManager
        )
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this._processFactory = processFactory ?? throw new ArgumentNullException(nameof(processFactory));
            this._notificationResourceManager = notificationResourceManager ?? throw new ArgumentNullException(nameof(notificationResourceManager));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                this._logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // fetch the mail tracking objects in pending state
                var mails = await this._notificationResourceManager.GetPendingMailAndMarkForProcessing();

                if(mails != null && mails.Any())
                {
                    var sendMailEvent = new SendMailEvent
                    {
                        id = Guid.NewGuid(),
                        event_date_utc = DateTime.UtcNow,
                        mails_to_send = mails,
                    };

                    await this._processFactory
                        .Create(ProcessTypeEnu.SendMailProcess)
                        .SetEvent(sendMailEvent)
                        .SetLogger(this._logger)
                        .Execute();
                }

                await Task.Delay(1000 * 60 * 5, stoppingToken);
            }
        }
    }
}
