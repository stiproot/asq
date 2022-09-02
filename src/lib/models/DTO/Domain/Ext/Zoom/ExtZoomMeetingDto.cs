using DTO.Zoom.Meeting;
using System.Text.Json;

namespace DTO.Domain.Ext.Zoom
{
    public class ExtZoomMeetingDto : BaseExtZoomDto
    {
        //public long MeetingId{ get; set; }

        public CreateMeetingResponse PayloadSerializer
        {
            get => JsonSerializer.Deserialize<CreateMeetingResponse>(this.Payload);
            set => this.Payload = JsonSerializer.Serialize(value);
        } 
    }
}