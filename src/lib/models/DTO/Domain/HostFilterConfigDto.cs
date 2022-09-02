using System.Linq;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class HostFilterConfigDto
    {
        public IEnumerable<short> Foci{ get; set; }

        public string Elastic{ get; set; }
        public string Name{ get; set; }

        // modifying result
        public int? RequestingUserUtcOffset{ get; set; }
        public int? Take{ get; set; }

        public string GenerateCacheKey() 
            =>  $"{nameof(HostFilterConfigDto)}::" +
                $"{nameof(this.Foci)}:{System.String.Join(",", this.Foci)};" +
                $"{nameof(this.Elastic)}:{this.Elastic};";
    }
}