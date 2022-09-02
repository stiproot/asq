namespace DTO.Domain
{
    public class MailDto : BaseDomainDto 
    { 
        public string Subject{ get; set; }
        public string Body{ get; set; }
        public string ToEmailAddress{ get; set; }
        public string FromEmailAddress{ get; set; }
        public short StatusId{ get; set; }
    }
}