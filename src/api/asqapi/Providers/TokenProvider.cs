using Microsoft.AspNetCore.Http;

namespace asqapi.Providers
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenProvider(
            IHttpContextAccessor httpContextAccessor
        )
        {
            this._httpContextAccessor = httpContextAccessor ?? throw new System.ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetBearerToken()
        {
            var authHeader = this.GetAuthorizationHeader();
            var bearerToken = authHeader.Split(" ")[1];
            return bearerToken;
        }

        public string GetAuthorizationHeader()
        {
            var request = this._httpContextAccessor.HttpContext.Request;
            var authHeader = request.Headers["Authorization"];
            return authHeader;
        }
    }
}