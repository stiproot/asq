using DTO.Zoom.User;
using System.Text.Json;

namespace DTO.Domain.Ext.Zoom
{
    public class ExtZoomUserDto : BaseExtZoomDto
    {
        //public long HostId{ get; set; }
        public CreateUserResponse DeserializedPayload => JsonSerializer.Deserialize<CreateUserResponse>(this.Payload);
    }
}