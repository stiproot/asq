using System;
using Microsoft.Extensions.Configuration;
using ZoomClient.Builder;

namespace ZoomClient.Factory
{
    public class JwtBuilderFactory : IJwtBuilderFactory
    {
        private readonly IConfiguration _config;

        public JwtBuilderFactory(IConfiguration config) 
        {
            _config = config;
        }

        public IJwtBuilder Create()
        {
            return new JwtBuilder(_config);
        }
    }
}