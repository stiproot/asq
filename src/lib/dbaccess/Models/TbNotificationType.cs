﻿using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbNotificationType
    {
        public TbNotificationType()
        {
            TbNotifications = new HashSet<TbNotification>();
        }

        public short Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbNotification> TbNotifications { get; set; }
    }
}
