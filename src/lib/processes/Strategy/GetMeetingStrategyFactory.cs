using managers.Resource;
using processes.Engine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace processes.Strategy
{
    public class GetMeetingStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingIdParamName = "meetingId";
        private readonly IMeetingResourceManager _manager;

        public GetMeetingStrategyFactory(

            IEnginePacketFactory packetFactory,
            IMeetingResourceManager resourceManager
        ): base(packetFactory) => this._manager = resourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
            => async () => 
                {
                    var uniqueId = param[_meetingIdParamName].Cast<Guid>();

                    var meeting = await _manager.GetMeeting(uniqueId);

                    return this._packetFactory.Create(meeting, this._nextParamName);
                };
    }
}