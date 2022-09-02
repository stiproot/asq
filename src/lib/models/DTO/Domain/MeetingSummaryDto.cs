namespace DTO.Domain
{
    public class MeetingSummaryDto: BaseDomainDto
    {
        public string Title{ get; set; }
        public string Description{ get; set; }
        public short MeetingStatusId{ get; set; }
        public string StatusDescription{ get; set; }
        public System.DateTime StartDateUtc{ get; set; }
        public int EstimatedDuration{ get; set; }
        public int TimezoneId{ get; set; }
        public System.Guid CreationUserUniqueId{ get; set; }
        public string CreationUserName{ get; set; }
        public string CreationUserSurname{ get; set; }
        public string CreationUserUsername{ get; set; }
        public string ThumbnailUrl{ get; set; }
        public string CreationUserThumbnailUrl{ get; set; }
        public bool HasPassed{ get; set; }
        public bool HasRecordings{ get; set; }
    }
}
