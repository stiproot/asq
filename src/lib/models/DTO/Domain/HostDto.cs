using DTO.Domain.Ext.Zoom;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class HostDto : BaseDomainDto
    {
        public string Company{ get; set; }
        public string CareerSummary{ get; set; }
        public long ExtUserId{ get; set; }
        public bool? IsConsultant{ get; set; }
        public ICollection<FocusHostMappingDto> Specialities{ get; set; }

        public ExtZoomUserDto ExtUser{ get; set; }
    }
}