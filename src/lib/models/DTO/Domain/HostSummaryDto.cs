using System.Collections.Generic;

namespace DTO.Domain
{
    public class HostSummaryDto: BaseDomainDto
    {
        public string Username{ get; set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
        public string Summary{ get; set; }
        public string ThumbnailUrl{ get; set; }
        //public IEnumerable<string> Foci{ get; set; }
    }
}
