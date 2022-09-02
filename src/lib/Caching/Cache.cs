using Caching.Abstractions;
using Caching.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace Caching
{
    public class Cache: ICache
    {
        private readonly ILogger<Cache> _logger;
        private readonly IMemoryCache _memoryCache;
        private readonly IMemoryCacheFactory _memoryCacheFactory;
        private readonly IOptions<CacheSettings> _options;
        private readonly IMemoryCacheEntryOptionsFactory _memoryCacheEntryOptionsFactory;
        private readonly Locker _locker = new Locker();

        public Cache(
            ILogger<Cache> logger,
            IMemoryCacheFactory memoryCacheFactory,
            IOptions<CacheSettings> options,
            IMemoryCacheEntryOptionsFactory memoryCacheEntryOptionsFactory
        )
        {
            this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
            this._memoryCacheFactory = memoryCacheFactory ?? throw new System.ArgumentNullException(nameof(memoryCacheFactory));
            this._options = options ?? throw new System.ArgumentNullException(nameof(options));
            this._memoryCacheEntryOptionsFactory = memoryCacheEntryOptionsFactory ?? throw new System.ArgumentNullException(nameof(memoryCacheEntryOptionsFactory));

            this._memoryCache = this._memoryCacheFactory.Create();
        }

        private sealed class Locker
        {
            private readonly ConcurrentDictionary<object, SemaphoreSlim> _dictionary = new ConcurrentDictionary<object, SemaphoreSlim>();
            public SemaphoreSlim this[object key] => this._dictionary.GetOrAdd(key, _ => new SemaphoreSlim(1, 1));
        }

        public (bool, T) Get<T>(object key)
        {
            var isCached = this._memoryCache.TryGetValue<T>(key, out T cachedValue);
            return (isCached, cachedValue);
        }

        public async Task SetAsync<T>(
            object key, 
            T value, 
            System.DateTime? absoluteExpiration = null
        )
        {
            await this._locker[key].WaitAsync().ConfigureAwait(false);
            try
            {
                var (isCached, cachedValue) = this.Get<T>(key);
                if(isCached) return;

                var options = this._memoryCacheEntryOptionsFactory.Create(absoluteExpiration);
                this._memoryCache.Set<T>(key, value, options);
            }
            finally
            {
                this._locker[key].Release();
            }
        }

        public async Task<T> GetOrCreateAsync<T>(
            object key, 
            System.Func<Task<T>> factory, 
            System.DateTime? absouteExpiration = null
        )
        {
            var (isCached, cachedValue) = this.Get<T>(key);
            if(isCached) return cachedValue;

            await this._locker[key].WaitAsync().ConfigureAwait(false);

            try
            {
                (isCached, cachedValue) = this.Get<T>(key);
                if(isCached) return cachedValue;

                var result = await factory();
                var options = this._memoryCacheEntryOptionsFactory.Create(absouteExpiration);
                this._memoryCache.Set<T>(key, result, options);
                return result;
            }
            finally
            {
                this._locker[key].Release();
            }
        }
    }
}
