﻿using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbFocusHostMapping
    {
        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public long HostId { get; set; }
        public short FocusId { get; set; }

        public virtual TbFocus Focus { get; set; }
        public virtual TbHost Host { get; set; }
    }
}
