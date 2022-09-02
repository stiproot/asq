namespace DTO.Domain
{
    public class VideoSummaryDto: BaseDomainDto
    {
        public string Title{ get; set; }
        public string Description{ get; set; }
        public System.Guid CreationUserUniqueId{ get; set; }
        public string CreationUserName{ get; set; }
        public string CreationUserSurname{ get; set; }
        public string CreationUserUsername{ get; set; }
        public string ThumbnailUrl{ get; set; }
        public string CreationUserThumbnailUrl{ get; set; }
        public string Url{ get; set; }
    }
}
