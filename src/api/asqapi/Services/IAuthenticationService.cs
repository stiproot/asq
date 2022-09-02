using System.Threading.Tasks;
using DTO.Domain;

namespace asqapi.Services
{
    public interface IAuthenticationService
    {
        Task<UserDto> Authenticate(string username, string password);
    }
}