using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;
using System;

namespace dbaccess.Repository
{
    public interface IUserResourceAccess
    {
        Task<UserDto> GetUser(object id);
        Task<UserDto> GetHost(object id);
        Task<IEnumerable<HostSummaryDto>> GetHostSummariesByFilter(HostFilterConfigDto filter);
        Task ActivateUser(Guid token);
        Task<UserDto> ActivateAndReturnUser(Guid token);
        Task<UserDto> GetUser(string username, string password);
        Task<UserDto> AddUser(UserDto dto);
        Task DeleteUser(object id);
        Task UpdateUser(UserDto dto);
    }
}