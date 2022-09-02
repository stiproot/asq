using System;

namespace DTO.Domain
{
    public class PaymentInfoDto : BaseDomainDto
    {
        public string CardNumber{ get; set; }
        public DateTime ExpirationDate{ get; set; }
        public string Cvc{ get; set; }
        public short CardTypeId{ get; set; }
        public CardTypeDto CardType{ get; set; } 
    }
}