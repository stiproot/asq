using Caching.Abstractions;
using Caching.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Factory
{
    public class MemoryCacheEntryOptionsFactory: IMemoryCacheEntryOptionsFactory
    {
        private readonly ILogger<MemoryCacheEntryOptionsFactory> _logger;
        private readonly IOptions<CacheSettings> _options;

        public MemoryCacheEntryOptionsFactory(
            ILogger<MemoryCacheEntryOptionsFactory> logger,
            IOptions<CacheSettings> options
        )
        {
            this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            this._options = options ?? throw new System.ArgumentNullException(nameof(options));
        }

        public MemoryCacheEntryOptions Create(System.DateTime? absoluteExpiration = null)
            => new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = absoluteExpiration,
                SlidingExpiration = System.TimeSpan.FromMinutes(this._options.Value.SlidingExpirationMinutes)
            };
    }
}
