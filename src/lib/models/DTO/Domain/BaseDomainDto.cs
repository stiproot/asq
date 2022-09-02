using System;

namespace DTO.Domain
{
    public class BaseDomainDto
    {
        public long Id{ get; set; }
        public Guid UniqueId{ get; set; }
        public DateTime CreationDateUtc{ get; set; }
        public long CreationUserId{ get; set; } = 0;
        public bool Inactive{ get; set; }

        public BaseDomainDto() =>
            (this.Id, this.UniqueId, this.CreationUserId, this.CreationDateUtc, this.Inactive) = 
            (0, Guid.NewGuid(), 0, DateTime.UtcNow, false);

        public bool Validate(bool throwException = true)
        {
            bool invalid = this.UniqueId == Guid.Empty || this.CreationDateUtc == null;
            if(invalid && throwException) throw new Exception($"Invalid {nameof(BaseDomainDto)} structure");
            return invalid;
        }
    }
}