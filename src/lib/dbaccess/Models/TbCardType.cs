using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbCardType
    {
        public TbCardType()
        {
            TbPaymentInfos = new HashSet<TbPaymentInfo>();
        }

        public int Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TbPaymentInfo> TbPaymentInfos { get; set; }
    }
}
