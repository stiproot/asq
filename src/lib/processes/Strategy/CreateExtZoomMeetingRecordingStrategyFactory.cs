using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using managers.Resource;
using processes.Engine;

namespace processes.Strategy
{
    public class CreateExtZoomMeetingRecordingStrategyFactory: BaseStrategyFactory
    {
        private const string _getZoomMeetingRecordingResponseParamName = "getZoomMeetingRecordingResponse";
        private const string _extMeetingIdParamName = "extMeetingId";
        private readonly IMeetingResourceManager _meetingResourceManager;

        public CreateExtZoomMeetingRecordingStrategyFactory(
            IEnginePacketFactory packetFactory,
            IMeetingResourceManager meetingResourceManager
        ): base(packetFactory) 
            => this._meetingResourceManager = meetingResourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var recordingResponse = param[_getZoomMeetingRecordingResponseParamName].Cast<DTO.Zoom.Meeting.GetMeetingRecordingsResponse>();
                var extMeetingId = param[_extMeetingIdParamName].Cast<long>();

                var extZoomMeetingRecording = new DTO.Domain.Ext.Zoom.ExtZoomMeetingRecordingDto()
                {
                    Payload = System.Text.Json.JsonSerializer.Serialize(recordingResponse),
                    ExtMeetingId = extMeetingId 
                };

                var extZoomMeeting = await this._meetingResourceManager.AddExtMeetingRecording(extZoomMeetingRecording);

                return this._packetFactory.Create(extZoomMeeting, this._nextParamName);
            };
        }
    }
}