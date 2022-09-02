using System;
using System.Collections.Generic;
using extensions;

namespace DTO.Domain
{
    public class UserDto: BaseDomainDto
    {
        public string Username{ get; set; }
        public string Password{ get; set; }
        public string Email{ get; set; }
        public string Name{ get; set; }
        public string Surname{ get; set; }
        public long? PaymentInfoId {get; set; }
        public long? HostId {get; set; }
        public long ImgId {get; set; }
        public int TimezoneId {get; set; }
        public AccountTypeEnu AccountType { get; set; }

        public PaymentInfoDto PaymentInfo{ get; set; }
        public HostDto Host{ get; set; }
        public ImgDto Img{ get; set; }
        public TimezoneDto Timezone{ get; set; }
        public ICollection<FocusUserMappingDto> Interests{ get; set; }
        public string Token{ get; set; }

        new public bool Validate(bool throwException = true)
        {
            base.Validate(throwException);

            bool invalid = this.Username.IsNullOrEmpty()
                || this.Password.IsNullOrEmpty()
                || this.Name.IsNullOrEmpty()
                || this.Surname.IsNullOrEmpty()
                || (this.Interests == null || this.Interests.Count == 0)
                || this.Img == null
                || this.TimezoneId <= 0;

            if(invalid && throwException)
            {
                throw new Exception("Invalid user structure");
            }

            return invalid;
        }
    }
}