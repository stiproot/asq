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
    public class CreateUserStrategyFactory: BaseStrategyFactory
    {
        private const string _userParamName = "user";
        private const string _extUserParamName = "extUser";
        private readonly IUserResourceManager _manager;

        private Action<UserDto, CreateUserResponse> _consolidateHandle = (UserDto u, CreateUserResponse h) => 
        {
            if(h != null)
            {
                u.Host.ExtUser = new DTO.Domain.Ext.Zoom.ExtZoomUserDto()
                {
                    Payload = JsonSerializer.Serialize(h)
                };
            }
        };

        public CreateUserStrategyFactory(
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
                    extUser = param[_extUserParamName]?.Cast<CreateUserResponse>();
                }

                if(this._consolidateHandle != null)
                {
                    this._consolidateHandle(user, extUser);
                }

                await _manager.AddUser(user);

                return null;
            };
        }
    }
}