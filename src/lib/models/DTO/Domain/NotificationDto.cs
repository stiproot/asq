namespace DTO.Domain
{
    public class NotificationDto: BaseDomainDto
    {
        public long UserId{ get; set; }
        public bool Seen{ get; set; }
        public string Title{ get; set; }
        public string Message{ get; set; }
        public string ImgUrl{ get; set; }
        public string VideoUrl{ get; set; }
        public string MeetingUrl{ get; set; }
        public string ExtMeetingUrl{ get; set; }
        public short NotificationTypeId{ get; set; }
    }
}
