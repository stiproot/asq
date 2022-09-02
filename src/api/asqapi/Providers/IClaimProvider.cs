using System;
using System.Security.Claims;

namespace asqapi.Providers
{
    public interface IClaimProvider
    {
        string Username(ClaimsPrincipal user);
        string Email(ClaimsPrincipal user);
        string Name(ClaimsPrincipal user);
        string Surname(ClaimsPrincipal user);
        long? UserId(ClaimsPrincipal user);
        Guid? UserGuid(ClaimsPrincipal user);
        long? HostId(ClaimsPrincipal user);
        bool isHost(ClaimsPrincipal user);
        int? UtcOffset(ClaimsPrincipal user);
        string Timezone(ClaimsPrincipal user);
    }
}