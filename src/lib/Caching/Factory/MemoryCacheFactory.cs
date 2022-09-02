using Caching.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;

namespace Caching.Factory
{
    public class MemoryCacheFactory: IMemoryCacheFactory
    {
        private readonly ILogger<MemoryCacheFactory> _logger;
        private readonly IMemoryCacheOptionsFactory _memoryCacheOptionsFactory;

        public MemoryCacheFactory(
            ILogger<MemoryCacheFactory> logger,
            IMemoryCacheOptionsFactory memoryCacheOptionsFactory
        )
        {
            this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            this._memoryCacheOptionsFactory = memoryCacheOptionsFactory ?? throw new System.ArgumentNullException(nameof(memoryCacheOptionsFactory));
        }

        public IMemoryCache Create()
            => new MemoryCache(this._memoryCacheOptionsFactory.Create());
    }
}
