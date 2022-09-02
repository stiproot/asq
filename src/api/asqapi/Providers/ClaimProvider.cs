using System;
using System.Security.Claims;
using System.Linq;
using asqapi.Models;
using asqapi.Constants;

namespace asqapi.Providers
{
    public class ClaimProvider: IClaimProvider
    {
        public string Username(ClaimsPrincipal _user) => _user.FindFirst(ClaimTypes.NameIdentifier).Value;

        public string Email(ClaimsPrincipal _user) => _user.FindFirst(ClaimTypes.Email).Value;

        public string Name(ClaimsPrincipal _user) => _user.FindFirst(ClaimTypes.Name).Value;

        public string Surname (ClaimsPrincipal _user) => _user.FindFirst(ClaimTypes.Surname).Value;

        public long? UserId (ClaimsPrincipal _user)
        {
            if(long.TryParse(_user.Claims.FirstOrDefault(c => c.Type == ClaimTypeNames.USER_ID)?.Value, out long id)) return id;
            return null;
        }

        public Guid? UserGuid (ClaimsPrincipal _user)
        {
            if(Guid.TryParse(_user.Claims.FirstOrDefault(c => c.Type == ClaimTypeNames.USER_GUID)?.Value, out Guid id)) return id;
            return null;
        }

        public long? HostId (ClaimsPrincipal _user)
        {
            if(long.TryParse(_user.Claims.FirstOrDefault(c => c.Type == ClaimTypeNames.HOST_ID).Value, out long id)) return id;
            return null;
        }

        public bool isHost (ClaimsPrincipal _user) => _user.IsInRole(Role.HOST);

        public int? UtcOffset(ClaimsPrincipal _user)
        { 
            if(int.TryParse(_user.Claims.FirstOrDefault(c => c.Type == ClaimTypeNames.UTC_OFFSET)?.Value, out int offset)) return offset;
            return null;
        }

        public string Timezone(ClaimsPrincipal _user) => _user.Claims.FirstOrDefault(c => c.Type == ClaimTypeNames.TIMEZONE)?.Value;

        public ClaimProvider() { }
    }
}
