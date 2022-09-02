using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Zoom.User;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class CreateZoomUserStrategyFactory: BaseStrategyFactory
    {
        private const string _hostConfigParamName = "hostConfig";
        private readonly IZoomResourceManager _resourceManager;

        public CreateZoomUserStrategyFactory(
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        ): base(packetFactory) => this._resourceManager = zoomResourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var p = param[_hostConfigParamName].Cast<CreateUserRequest>();
                var resp = await this._resourceManager.CreateUser(p);
                //string testRespStr = await Task.FromResult<string>("{\"id\":\"6A42KiD5SoGVA2z-ru9jSw\",\"first_name\":\"ash\",\"last_name\":\"catchum\",\"email\":\"simon@asq.properties.co.za\",\"type\":1}");
                //var resp = JsonSerializer.Deserialize<CreateUserResponse>(testRespStr);
                return this._packetFactory.Create(resp, this._nextParamName);
            };
        }
    }
}