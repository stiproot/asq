using ZoomClient.Factory;
using ZoomClient.Builder;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System;

namespace ZoomClient
{
    public class GenericHttpClient : IGenericHttpClient
    {
        private HttpClient _client;
        private readonly IJwtBuilderFactory _jwtBuilderFactory; 
        private readonly IRequestBodyBuilder _requestBodyBuilder;
        public GenericHttpClient(IJwtBuilderFactory jwtBuilderFactory, IRequestBodyBuilder requestBodyBuilder)
        {
            this._client = new HttpClient();
            this._jwtBuilderFactory = jwtBuilderFactory;
            this._requestBodyBuilder = requestBodyBuilder;
        }

        private void RefreshBearerToken()
        {
            string token = _jwtBuilderFactory.Create().SetExpiration(DateTime.Now.AddMinutes(15)).Build();

            //Console.WriteLine(token);

            this._client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T> Get<T>(string url)
        {
            this.RefreshBearerToken();

            var response = await _client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync();

            //Console.WriteLine(responseAsString);

            return JsonSerializer.Deserialize<T>(responseAsString);
        }

        public async Task<T> Post<T>(string url, object obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            this.RefreshBearerToken();

            var response = await _client.PostAsync(url, _requestBodyBuilder.BuildRequestBody(obj));

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task Patch(string url, object obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            this.RefreshBearerToken();

            var response = await _client.PatchAsync(url, _requestBodyBuilder.BuildRequestBody(obj));

            response.EnsureSuccessStatusCode();
        }

        public async Task<T> Put<T>(string url, object obj)
        {
            if(obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            this.RefreshBearerToken();

            var response = await _client.PutAsync(url, _requestBodyBuilder.BuildRequestBody(obj));

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task<T> Delete<T>(string url)
        {
            this.RefreshBearerToken();

            var response = await _client.DeleteAsync(url);

            response.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync());
        }

        public async Task Delete(string url)
        {
            this.RefreshBearerToken();

            (await _client.DeleteAsync(url)).EnsureSuccessStatusCode();
        }
    }
}
