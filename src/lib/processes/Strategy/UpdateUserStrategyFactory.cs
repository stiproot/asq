using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Zoom.User;
using DTO.Domain;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class UpdateUserStrategyFactory: BaseStrategyFactory
    {
        private const string _userParamName = "user";
        private const string _extUserParamName = "extUser";
        private readonly IUserResourceManager _manager;
        private Action<UserDto, CreateUserResponse> _consolidateHandle = (UserDto user, CreateUserResponse extUser) => 
        {
            if(extUser != null)
            {
                user.Host.ExtUser = new DTO.Domain.Ext.Zoom.ExtZoomUserDto()
                {
                    Payload = JsonSerializer.Serialize(extUser),
                    //HostId = 0,
                };
            }

            if(user.AccountType == AccountTypeEnu.STUDENT)
            {
                if(user.Host != null)
                {
                    user.Host = null;
                }
                if(user.HostId != null)
                {
                    user.HostId = null;
                }
            }

            if(user.Timezone != null)
            {
                user.Timezone = null;
            }
        };

        public UpdateUserStrategyFactory(
            IEnginePacketFactory packetFactory,
            IUserResourceManager resourceManager
        ): base(null) => this._manager = resourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var user = param[_userParamName].Cast<UserDto>();
                CreateUserResponse extUser = null;

                if(param.ContainsKey(_extUserParamName))
                {
                    extUser = param[_extUserParamName].Cast<CreateUserResponse>();
                }

                if(this._consolidateHandle != null)
                {
                    this._consolidateHandle(user, extUser);
                }

                await _manager.UpdateUser(user);

                return null;
            };
        }
    }
}