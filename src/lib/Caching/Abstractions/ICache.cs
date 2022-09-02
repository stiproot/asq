using System.Threading.Tasks;

namespace Caching.Abstractions
{
    public interface ICache
    {
        (bool, T) Get<T>(object key);
        Task SetAsync<T>(
            object key, 
            T value, 
            System.DateTime? absoluteExpiration = null
        );
        Task<T> GetOrCreateAsync<T>(
            object key, 
            System.Func<Task<T>> factory, 
            System.DateTime? absouteExpiration = null
        );
    }
}
