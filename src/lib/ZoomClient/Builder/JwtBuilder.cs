using ZoomClient.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System;

namespace ZoomClient.Builder
{
    public class JwtBuilder : IJwtBuilder
    {
        private readonly IConfiguration _configuration;
        private readonly IZoomSettingProvider _zoomSettingProvider;
        private DateTime _expiration;
        private string _apiKeyRaw => _configuration["Zoom:WebApi:Config:ApiKey"];
        private string _apiSecretRaw => _configuration["Zoom:WebApi:Config:ApiSecret"];
        private SymmetricSecurityKey _symetricSecurityKey 
            => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSecretRaw));
        
        public JwtBuilder(IConfiguration configuration)
        {

            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public IJwtBuilder SetExpiration(DateTime expiration)
        {
            this._expiration = expiration;
            return this;
        }

        private SigningCredentials GenerateSigningCredentials()
        {
            return new SigningCredentials(_symetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }

        public string Build()
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = new JwtSecurityToken(issuer: _apiKeyRaw,
                                                          expires: _expiration,
                                                          signingCredentials: GenerateSigningCredentials());
            
            return handler.WriteToken(token);
        }
    }
}