namespace Caching.Models
{
    public class CacheSettings
    {
        public int ExpirationScanFrequencyMinutes{ get; set; }
        public int SlidingExpirationMinutes{ get; set; }
    }
}