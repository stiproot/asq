namespace asqapi.Providers
{
    public interface ITokenProvider
    {
        string GetBearerToken();
        string GetAuthorizationHeader();
    }
}