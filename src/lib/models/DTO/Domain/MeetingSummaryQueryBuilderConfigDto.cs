namespace DTO.Domain
{
    public class MeetingSummaryQueryBuilderConfigDto
    {
        public short MeetingStatusId{ get; set; }
        public System.Guid? CreationUserUniqueId{ get; set; }

        public string GenerateCacheKey() 
            => $"{nameof(MeetingSummaryQueryBuilderConfigDto)}::" +
            $"{nameof(this.MeetingStatusId)}:{this.MeetingStatusId};" + 
            $"{nameof(this.CreationUserUniqueId)}:{this.CreationUserUniqueId}";
    }
}