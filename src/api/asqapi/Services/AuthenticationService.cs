using asqapi.Models;
using asqapi.Constants;
using DTO.Domain;
using managers.Resource;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using System;

namespace asqapi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _config;
        private readonly IUserResourceManager _userResourceManager;

        public AuthenticationService(
            IConfiguration config, 
            IUserResourceManager userResourceManager
        ) 
        {
            this._config = config ?? throw new ArgumentNullException(nameof(config));
            this._userResourceManager = userResourceManager ?? throw new ArgumentNullException(nameof(userResourceManager));
        }

        private string MapRole(AccountTypeEnu accType)
        {
            return accType switch
            {
                AccountTypeEnu.HOST     => Role.HOST,
                AccountTypeEnu.STUDENT  => Role.STUDENT,
                _                       => throw new ArgumentException(message: "Invalid account type provided", paramName: nameof(accType))
            };
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            var user = await _userResourceManager.GetUser(username, password);

            if(user == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["AppSettings:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Username),
                    new Claim(ClaimTypes.Role, this.MapRole(user.AccountType)),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Surname, user.Surname),
                    new Claim(ClaimTypeNames.HOST_ID, (user.HostId ?? -1).ToString()),
                    new Claim(ClaimTypeNames.USER_ID, user.Id.ToString()),
                    new Claim(ClaimTypeNames.USER_GUID, user.UniqueId.ToString()),
                    new Claim(ClaimTypeNames.UTC_OFFSET, user.Timezone.UtcOffset.ToString()),
                    new Claim(ClaimTypeNames.TIMEZONE, user.Timezone.Display)
                }),
                Expires = DateTime.UtcNow.AddDays(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}