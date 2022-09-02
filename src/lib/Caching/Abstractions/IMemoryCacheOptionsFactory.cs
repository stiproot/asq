using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Caching.Abstractions
{
    public interface IMemoryCacheOptionsFactory
    {
        MemoryCacheOptions Create(System.TimeSpan? expirationScanFrequency = null);
    }
}
