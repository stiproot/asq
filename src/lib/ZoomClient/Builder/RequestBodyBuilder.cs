using CONST = ZoomClient.Constants.Constants;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace ZoomClient.Builder
{
    public class RequestBodyBuilder : IRequestBodyBuilder
    {
        public RequestBodyBuilder(){ }

        public HttpContent BuildRequestBody(object obj)
            => new StringContent(JsonSerializer.Serialize(obj), Encoding.UTF8, CONST.JsonMimeType);
    }
}