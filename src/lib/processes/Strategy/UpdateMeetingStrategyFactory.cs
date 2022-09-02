using DTO.Domain;
using managers.Resource;
using processes.Engine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace processes.Strategy
{
    public class UpdateMeetingStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingParamName = "meeting";
        private const string _extMeetingParamName = "extMeeting";
        private readonly IMeetingResourceManager _manager;

        private Action<MeetingDto> _consolidateHandle = (MeetingDto m) => 
        {
            if(m.Timezone != null)
            {
                m.Timezone = null;
            }
        };

        public UpdateMeetingStrategyFactory(
            IMeetingResourceManager resourceManager
        ): base(null) => this._manager = resourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var meeting = param[_meetingParamName].Cast<MeetingDto>();

                this._consolidateHandle(meeting); 

                await _manager.UpdateMeeting(meeting);

                return null;
            };
        }
    }
}