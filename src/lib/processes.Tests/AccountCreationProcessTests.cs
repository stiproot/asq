using DTO.Domain;
using DTO.Events;
using managers.Resource;
using processes.Process;
using processes.Factory;
using dbaccess.Factory.Test;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace processes.Tests
{
    public class AccountCreationProcessTests
    {
        private readonly ILogger _logger;
        private const bool _runHostCreationProcess = false;
        private readonly IUserFactory _userFactory;
        private readonly IAccountResourceManager _accountManager;
        private readonly IProcessFactory _processFactory;
        private readonly ITestOutputHelper _output;

        public AccountCreationProcessTests(
            ILogger<AccountCreationProcessTests> logger,
            IUserFactory userFactory,
            IAccountResourceManager accountManager,
            IProcessFactory processFactory,
            ITestOutputHelper output)
        {
            this._logger = logger;
            this._userFactory = userFactory;
            this._accountManager = accountManager;
            this._processFactory = processFactory;
            this._output = output;
        }

        [Fact]
        public void CreateHostTest()
        {
            try
            {
                if(!_runHostCreationProcess) return;

                Task t = CreateHostUserProcess();
                t.Wait();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task CreateHostUserProcess()
        {
            // arrange
            var user = this._userFactory.GenerateUserDto(accountType: AccountTypeEnu.HOST);
            var @event = new AccountCreationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                user = user,
            };

            // act
            user.Validate();

            var process = _processFactory.Create(ProcessTypeEnu.CreateAccountProcess)
                                    .SetEvent(@event)
                                    .SetLogger(_logger)
                                    .Init();

            await process.Execute();

            var @mailEvent = new SendNotificationEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                notification_config = new DTO.Notification.NotificationConfig
                {
                    ToEmailAddress = user.Email,
                    ToName = user.Name,
                    ToSurname = user.Surname,
                    ToUniqueId = user.UniqueId,
                    ToUsername = user.Username,
                    NotificationType = DTO.Notification.NotificationTypeEnu.EMAIL_CONFIRMATION
                },
            };

            var mailProcess = this._processFactory.Create(ProcessTypeEnu.QueueNotificationProcess)
                                        .SetEvent(@mailEvent)
                                        .Init();

            await mailProcess.Execute();
        }
    }
}
