using Caching.Abstractions;
using Caching.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Factory
{
    public class MemoryCacheOptionsFactory: IMemoryCacheOptionsFactory
    {
        private readonly ILogger<MemoryCacheOptionsFactory> _logger;
        private readonly IOptions<CacheSettings> _options;

        public MemoryCacheOptionsFactory(
            ILogger<MemoryCacheOptionsFactory> logger,
            IOptions<CacheSettings> options
        )
        {
            this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            this._options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public MemoryCacheOptions Create(System.TimeSpan? expirationScanFrequency = null)
            => new MemoryCacheOptions
            {
                ExpirationScanFrequency = expirationScanFrequency ?? System.TimeSpan.FromMinutes(this._options.Value.SlidingExpirationMinutes)
            };
    }
}
