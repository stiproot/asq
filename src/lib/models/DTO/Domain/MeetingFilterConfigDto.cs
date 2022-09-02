using System.Linq;

namespace DTO.Domain
{
    public class MeetingFilterConfigDto
    {
        public System.Collections.Generic.IEnumerable<short> Foci{ get; set; }
        public short? MeetingStatusId{ get; set; }
        public System.DateTime? StartDateUtc{ get; set; }
        public int? EstimatedDuration{ get; set; }
        public int? TimezoneId{ get; set; }
        public System.Guid? CreationUserUniqueId{ get; set; }
        public string CreationUserName{ get; set; }
        // elastic will search host name, host surname, title & decription
        public string Elastic{ get; set; }

        // modifying result
        public int? RequestingUserUtcOffset{ get; set; }
        public int? Take{ get; set; }

        public string GenerateCacheKey() 
            =>  $"{nameof(MeetingFilterConfigDto)}::" +
                $"{nameof(this.MeetingStatusId)}:{this.MeetingStatusId};" +
                $"{nameof(this.Foci)}:{System.String.Join(",", this.Foci)};" +
                $"{nameof(this.CreationUserUniqueId)}:{this.CreationUserUniqueId};" +
                $"{nameof(this.CreationUserName)}:{this.CreationUserName};" +
                $"{nameof(this.Elastic)}:{this.Elastic};";
    }
}