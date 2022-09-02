namespace DTO.Domain
{
    public class TimezoneDto : BaseDomainDto
    {
        public string Display{ get; set; }
        public short UtcOffset{ get; set; }
        public string ExtCode{ get; set; }
    }
}