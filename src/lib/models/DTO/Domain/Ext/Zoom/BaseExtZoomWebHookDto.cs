namespace DTO.Domain.Ext.Zoom
{
    public class BaseExtZoomWebHookDto : BaseDomainDto
    {
        public BaseExtZoomWebHookDto(): base(){ }

        public string Payload{ get; set; }
        public string EventType{ get; set; }
    }
}