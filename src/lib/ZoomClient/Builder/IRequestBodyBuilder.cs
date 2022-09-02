using System.Net.Http;

namespace ZoomClient.Builder
{
    public interface IRequestBodyBuilder
    {
        HttpContent BuildRequestBody(object obj);
    }
}