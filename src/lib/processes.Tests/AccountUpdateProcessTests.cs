using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;
using DTO.Tracking;
using DTO.Domain;
using DTO.Events;
using managers.Resource;
using processes.Factory;
using processes.Process.Account;
using dbaccess.Factory.Test;

namespace processes.Tests
{
    public class AccountUpdateProcessTests
    {
        private const bool _runUpgradeProcess = false;
        private const bool _runDowngradeProcess = false;
        private const bool _runUpdateImgProcess = false;
        private readonly IUserFactory _userFactory;
        private readonly IAccountResourceManager _accountManager;
        private readonly IUserResourceManager _userResourceManager;
        private readonly IProcessFactory _processFactory;
        private readonly ITestOutputHelper _output;

        public AccountUpdateProcessTests(
            IUserFactory userFactory,
            IAccountResourceManager accountManager,
            IUserResourceManager userResourceManager,
            IProcessFactory processFactory,
            ITestOutputHelper output)
        {
            this._userFactory = userFactory;
            this._accountManager = accountManager;
            this._userResourceManager = userResourceManager;
            this._processFactory = processFactory;
            this._output = output;
        }

        [Fact]
        public void Test1()
        {
            try
            {
                if(!_runDowngradeProcess) return;

                Task t = DowngradeHostToUserProcess();
                t.Wait();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task DowngradeHostToUserProcess()
        {
            long userId = 19;

            // arrange
            var user = await this._userResourceManager.GetUser(userId);
            if(user.AccountType != AccountTypeEnu.HOST)
            {
                throw new Exception("Downgrade test cannot run, as user is not a host");
            }
            user.Host = null;
            user.HostId = null;
            user.AccountType = AccountTypeEnu.STUDENT;

            var @event = new AccountUpdateEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                user = user,
            };

            // act
            user.Validate();
            var process = _processFactory.Create(Process.ProcessTypeEnu.UpdateAccountProcess)
                                    .SetEvent(@event)
                                    //.SetLogger(_logger)
                                    .Init();

            await process.Execute();
        }

        [Fact]
        public void Test2()
        {
            try
            {
                if(!_runUpgradeProcess) return;

                Task t = UpgradeUserToHostProcess();
                t.Wait();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpgradeUserToHostProcess()
        {
            long userId = 19;

            // arrange
            var user = await this._userResourceManager.GetUser(userId);
            if(user.AccountType != AccountTypeEnu.STUDENT)
            {
                throw new Exception("Upgrade test cannot run, as user is not a student");
            }

            user.Host = this._userFactory.GenerateUserDto(AccountTypeEnu.HOST).Host;
            user.HostId = 0;
            user.AccountType = AccountTypeEnu.HOST;

            var @event = new AccountUpdateEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                user = user,
            };

            // act
            user.Validate();
            var process = _processFactory.Create(Process.ProcessTypeEnu.UpdateAccountProcess)
                                    .SetEvent(@event)
                                    //.SetLogger(_logger)
                                    .Init();

            await process.Execute();
        }

        [Fact]
        public void UpdateImgTest()
        {
            try
            {
                if(!_runUpdateImgProcess) return;

                Task t = UpdateImgProcess();
                t.Wait();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateImgProcess()
        {
            long userId = 19;

            // arrange
            var user = await this._userResourceManager.GetUser(userId);
            Assert.NotNull(user);

            user.Img = this._userFactory.GenerateUserDto(AccountTypeEnu.HOST).Img;
            user.ImgId = 0;

            var @event = new AccountUpdateEvent
            {
                id = Guid.NewGuid(),
                event_date_utc = DateTime.UtcNow,
                user = user,
            };

            // act
            user.Validate();
            var process = _processFactory.Create(Process.ProcessTypeEnu.UpdateAccountProcess)
                                    .SetEvent(@event)
                                    //.SetLogger(_logger)
                                    .Init();

            await process.Execute();
        }
    }
}
