using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace Caching.Abstractions
{
    public interface IMemoryCacheEntryOptionsFactory
    {
        MemoryCacheEntryOptions Create(System.DateTime? absoluteExpiration = null);
    }
}
