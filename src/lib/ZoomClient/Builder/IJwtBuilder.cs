using System;

namespace ZoomClient.Builder
{
    public interface IJwtBuilder
    {
        string Build();
        IJwtBuilder SetExpiration(DateTime expiration);
    }
}