using System.Text.Json.Serialization;
using System.Text.Json;

namespace DTO.Domain.Ext.Zoom
{
    public class ExtZoomMeetingWebHookDto: BaseExtZoomWebHookDto
    {
        public long MeetingId{ get; set; }

        public ExtZoomMeetingWebHookDto(): base(){ }

        [JsonIgnore]
        public DTO.Zoom.WebHook.Meeting.BaseWebHookMeetingEvent PayloadSerializer
        {
            get => JsonSerializer.Deserialize<DTO.Zoom.WebHook.Meeting.BaseWebHookMeetingEvent>(this.Payload);
            set => this.Payload = JsonSerializer.Serialize(value);
        } 
    }
}