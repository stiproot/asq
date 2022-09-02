using DTO.Zoom.Meeting;
using DTO.Domain;
using managers.Resource;
using processes.Engine;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace processes.Strategy
{
    public class CreateMeetingStrategyFactory: BaseStrategyFactory
    {
        private const string _meetingParamName = "meeting";
        private const string _extMeetingParamName = "extMeeting";
        private readonly IMeetingResourceManager _manager;
        private Action<MeetingDto, CreateMeetingResponse> _consolidateHandle = (MeetingDto m, CreateMeetingResponse ext) 
            => {
                    //m.Img.Path = imgPaths.Item1;
                    //m.Img.ThumbnailUrl = imgPaths.Item2;
                    m.ExtMeeting = new DTO.Domain.Ext.Zoom.ExtZoomMeetingDto()
                    {
                        Payload = JsonSerializer.Serialize(ext)
                    };

                    if(m.Timezone != null) m.Timezone = null;
                };

        public CreateMeetingStrategyFactory(
            IMeetingResourceManager resourceManager
        ): base(null) => this._manager = resourceManager;

        public override Func<Task<IEnginePacket>> CreateFactory(IDictionary<string, IEnginePacket> param)
        {
            return async () => 
            {
                var meeting = param[_meetingParamName].Cast<MeetingDto>();
                var extMeeting = param[_extMeetingParamName].Cast<CreateMeetingResponse>();

                if(this._consolidateHandle != null)
                {
                    this._consolidateHandle(meeting, extMeeting); 
                }

                await _manager.AddMeeting(meeting);

                return null;
            };
        }
    }
}