using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Zoom.User;
using DTO.Domain;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class GetUserStrategyFactory: BaseStrategyFactory
    {
        private const string _userIdParamName = "userId";
        private Action<UserDto, Tuple<string, string>, CreateUserResponse> _consolidateHandle = null;
        private readonly IUserResourceManager _manager;

        public GetUserStrategyFactory(
            IEnginePacketFactory packetFactory,
            IUserResourceManager resourceManager
        ): base(packetFactory) => this._manager = resourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var userId = param[_userIdParamName].Cast<long>();

                var user = await _manager.GetUser(userId);

                return this._packetFactory.Create(user, this._nextParamName);
            };
        }
    }
}