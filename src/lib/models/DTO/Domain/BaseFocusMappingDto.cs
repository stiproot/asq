using System;
using System.Collections.Generic;

namespace DTO.Domain
{
    public class BaseFocusMappingDto: BaseDomainDto
    {
        public short FocusId{ get; set; }

        public FocusDto Focus{ get; set; }
    }
}