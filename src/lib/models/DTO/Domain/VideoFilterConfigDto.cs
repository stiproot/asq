using System.Linq;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class VideoFilterConfigDto
    {
        public System.Guid? CreationUserUniqueId{ get; set; }
        public IEnumerable<short> Foci{ get; set; }

        // Elastic will search user name, user surname & title...
        public string Elastic{ get; set; }
        public string CreationUserName{ get; set; }
        public string Title{ get; set; }
        public string Description{ get; set; }

        // modifying result
        public int? RequestingUserUtcOffset{ get; set; }
        public int? Take{ get; set; }

        public string GenerateCacheKey() 
            =>  $"{nameof(VideoFilterConfigDto)}::" +
                $"{nameof(this.Foci)}:{System.String.Join(",", this.Foci)};" +
                $"{nameof(this.CreationUserUniqueId)}:{this.CreationUserUniqueId};" +
                $"{nameof(this.CreationUserName)}:{this.CreationUserName};" +
                $"{nameof(this.Elastic)}:{this.Elastic};";
    }
}