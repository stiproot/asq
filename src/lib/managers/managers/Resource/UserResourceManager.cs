using System.Collections.Generic;
using System.Threading.Tasks;
using DTO.Domain;
using dbaccess.Repository;
using dbaccess.Repository.QueryEnrichment;
using System;

namespace managers.Resource
{
    public class UserResourceManager : IUserResourceManager
    {
        private readonly IUserQueryEnrichmentResourceAccess _userQueryEnrichmentResourceAccess;
        private readonly IUserResourceAccess _userResourceAccess;
        private readonly IFocusUserMappingResourceAccess _focusUserMappingResourceAccess;
        private readonly IFocusHostMappingResourceAccess _focusHostMappingResourceAccess;

        public UserResourceManager(
            IUserQueryEnrichmentResourceAccess userQueryEnrichmentResourceAccess,
            IUserResourceAccess userResourceAccess,
            IFocusUserMappingResourceAccess focusUserMappingResourceAccess,
            IFocusHostMappingResourceAccess focusHostMappingResourceAccess 
        )
        {
            this._userQueryEnrichmentResourceAccess = userQueryEnrichmentResourceAccess ?? throw new ArgumentNullException(nameof(userQueryEnrichmentResourceAccess)); 
            this._userResourceAccess = userResourceAccess ?? throw new ArgumentNullException(nameof(userResourceAccess));
            this._focusUserMappingResourceAccess = focusUserMappingResourceAccess ?? throw new ArgumentNullException(nameof(focusUserMappingResourceAccess));
            this._focusHostMappingResourceAccess = focusHostMappingResourceAccess ?? throw new ArgumentNullException(nameof(focusHostMappingResourceAccess));
        }

        public async Task<UserDto> GetUser(object id) 
            => await _userResourceAccess.GetUser(id);

        public async Task<UserDto> GetHost(object id) 
            => await _userResourceAccess.GetHost(id);

        public async Task<UserDto> GetUser(string username, string password) 
            => await _userResourceAccess.GetUser(username, password);

        public async Task ActivateUser(Guid token) 
            => await _userResourceAccess.ActivateUser(token);

        public async Task<UserDto> ActivateAndReturnUser(Guid token) 
            => await _userResourceAccess.ActivateAndReturnUser(token);

        public async Task AddUser(UserDto dto) 
            => await _userResourceAccess.AddUser(dto);

        public async Task UpdateUser(UserDto dto) 
            => await _userResourceAccess.UpdateUser(dto);

        public async Task<IEnumerable<HostQueryDto>> BuildHostSummaryBaseQueries() 
            => await this._userQueryEnrichmentResourceAccess.BuildHostSummaryBaseQueries();

        public async Task<IEnumerable<HostSummaryDto>> GetHostSummariesByFilter(HostFilterConfigDto config)
            => await this._userResourceAccess.GetHostSummariesByFilter(config);

        
        public async Task DeleteFocusUserMapping(IEnumerable<FocusUserMappingDto> dtos)
            => await this._focusUserMappingResourceAccess.DeleteFocusUserMapping(dtos);

        public async Task DeleteFocusHostMapping(IEnumerable<FocusHostMappingDto> dtos)
            => await this._focusHostMappingResourceAccess.DeleteFocusHostMapping(dtos);
    }
}