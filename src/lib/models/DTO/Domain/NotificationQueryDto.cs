namespace DTO.Domain
{
    public class NotificationQueryDto
    {
        public long UserId{ get; set; } 
        public long? OlderThanId{ get; set; }
    }
}