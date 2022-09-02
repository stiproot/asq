using System.Threading.Tasks;

namespace ZoomClient
{
    public interface IGenericHttpClient
    {
        //string GetJwtToken();
        Task<T> Get<T>(string url);
        Task<T> Post<T>(string url, object obj);
        Task<T> Put<T>(string url, object obj);
        Task Patch(string url, object obj);
        Task<T> Delete<T>(string url);
        Task Delete(string url);
    }
}