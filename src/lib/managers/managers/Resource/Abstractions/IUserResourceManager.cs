using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;
using System;

namespace managers.Resource
{
    public interface IUserResourceManager
    {
        Task<UserDto> GetUser(object id);
        Task<UserDto> GetHost(object id);
        Task<UserDto> GetUser(string username, string password);
        Task ActivateUser(Guid token);
        Task<UserDto> ActivateAndReturnUser(Guid token);
        Task AddUser(UserDto dto);
        Task UpdateUser(UserDto dto);
        Task<IEnumerable<HostQueryDto>> BuildHostSummaryBaseQueries();
        Task<IEnumerable<HostSummaryDto>> GetHostSummariesByFilter(HostFilterConfigDto config);
        Task DeleteFocusUserMapping(IEnumerable<FocusUserMappingDto> dtos);
        Task DeleteFocusHostMapping(IEnumerable<FocusHostMappingDto> dtos);
    }
}