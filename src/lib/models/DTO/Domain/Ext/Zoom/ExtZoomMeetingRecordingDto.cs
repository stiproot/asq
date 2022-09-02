using DTO.Zoom.Meeting;
using System.Text.Json;

namespace DTO.Domain.Ext.Zoom
{
    public class ExtZoomMeetingRecordingDto : BaseExtZoomDto
    {
        public long ExtMeetingId{ get; set; }
        public GetMeetingRecordingsResponse PayloadSerializer
        {
            get => JsonSerializer.Deserialize<GetMeetingRecordingsResponse>(this.Payload);
            set => this.Payload = JsonSerializer.Serialize(value);
        } 
    }
}