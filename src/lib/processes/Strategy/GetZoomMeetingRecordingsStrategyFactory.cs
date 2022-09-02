using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class GetZoomMeetingRecordingsStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingParamName = "meeting";
        private readonly IZoomResourceManager _resourceManager;

        public GetZoomMeetingRecordingsStrategyFactory(
            IEnginePacketFactory packetFactory,
            IZoomResourceManager zoomResourceManager
        ): base(packetFactory) => this._resourceManager = zoomResourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var meeting = param[_meetingParamName].Cast<DTO.Domain.MeetingDto>();

                var meetingRecordings = await this._resourceManager.GetMeetingRecordings(meeting.ExtMeeting.PayloadSerializer.id.ToString());

                return this._packetFactory.Create(meetingRecordings, this._nextParamName);
            };
        }
    }
}