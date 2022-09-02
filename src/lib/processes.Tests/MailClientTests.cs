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
using DTO.Notification;

namespace processes.Tests
{
    public class MailClientTests
    {
        private const bool _sendMail = false;
        private readonly INotificationResourceManager _notificationResourceManager;

        public MailClientTests(INotificationResourceManager notificationResourceManager)
        {
            this._notificationResourceManager = notificationResourceManager;
        }

        [Fact]
        public async Task TestMailClient()
        {
            if(!_sendMail) return;

            var config = new NotificationConfig
            {
                ToEmailAddress = "simon@asq.properties",
                ToName = "simon",
                ToSurname = "stipcich",
                ToUsername = "stipsmoosh"
            };

            //var config = new NotificationConfig
            //{
                //ToEmailAddress = "mark@asq.properties",
                //ToName = "mark",
                //ToSurname = "barocus",
                //ToUsername = "spark"
            //};

            //await this._notificationResourceManager.SendMail(config);
        }
    }
}
