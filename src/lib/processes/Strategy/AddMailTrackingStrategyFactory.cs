using DTO.Domain;
using managers.Resource;
using processes.Engine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace processes.Strategy
{
    public class AddMailStrategyFactory: BaseStrategyFactory
    {
        private const string _mailParamName = "mail";
        private readonly INotificationResourceManager _notificationResourceManager;

        public AddMailStrategyFactory(
            INotificationResourceManager notificationResourceManager
        ): base(null)
        {
            this._notificationResourceManager = notificationResourceManager ?? throw new ArgumentNullException(nameof(notificationResourceManager));
        }

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var dto = param[_mailParamName].Cast<MailDto>();
                await this._notificationResourceManager.AddMail(dto);
                return null;
            };
        }
    }
}