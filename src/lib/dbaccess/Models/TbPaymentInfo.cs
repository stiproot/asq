using System;
using System.Collections.Generic;

#nullable disable

namespace dbaccess.Models
{
    public partial class TbPaymentInfo
    {
        public TbPaymentInfo()
        {
            TbUsers = new HashSet<TbUser>();
        }

        public long Id { get; set; }
        public string UniqueId { get; set; }
        public DateTime CreationDateUtc { get; set; }
        public long CreationUserId { get; set; }
        public bool Inactive { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Cvc { get; set; }
        public int CardTypeId { get; set; }

        public virtual TbCardType CardType { get; set; }
        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
