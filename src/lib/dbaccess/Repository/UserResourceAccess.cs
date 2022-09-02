using dbaccess.Factory;
using dbaccess.Models;
using DTO.Domain;
using AutoMapper;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace dbaccess.Repository
{
    public class UserResourceAccess: BaseResourceAccess<UserDto, TbUser>, IUserResourceAccess
    {
        private readonly IEnumerable<string> _includes = new List<string>()
        {
            "TbFocusUserMappings.Focus",
            "Host.TbFocusHostMappings.Focus",
            "Host.ExtUser",
            "Timezone",
            "PaymentInfo",
            "Img"
        }; 

        public UserResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<UserDto> GetUser(object id)
        {
            Expression<Func<TbUser, bool>> predicate = null;
            TbUser entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                predicate = (TbUser u) => u.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbUser u) => u.UniqueId.Equals(guid.ToString());
            }
            else throw new NotImplementedException();

            entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);

            if(entity == null) return null;
            return this._mapper.Map<TbUser, UserDto>(entity);
        }

        public async Task<UserDto> GetUser(string username, string password)
        {
            Expression<Func<TbUser, bool>> predicate = 
                (TbUser u) => 
                    (
                        u.Username.Equals(username) || 
                        u.Email.Equals(username)
                    ) && 
                    u.Password.Equals(password) && !u.Inactive;

            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);
            return entity == null ? null : this._mapper.Map<TbUser, UserDto>(entity);
        }

        public async Task ActivateUser(Guid token)
        {
            Expression<Func<TbUser, bool>> predicate = (TbUser u) => u.UniqueId.Equals(token.ToString());
            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);

            if(entity == null) throw new ArgumentException("Invalid user activation token provided");
            if(entity.Inactive == false) throw new InvalidOperationException("User is already activated");

            entity.Inactive = false;

            await this.Update(this._mapper.Map<TbUser, UserDto>(entity));
        }

        public async Task<UserDto> ActivateAndReturnUser(Guid token)
        {
            Expression<Func<TbUser, bool>> predicate = (TbUser u) => u.UniqueId.Equals(token.ToString());
            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);
            if(entity == null) throw new ArgumentException("Invalid user activation token provided");
            if(entity.Inactive == false) throw new InvalidOperationException("User is already activated");

            entity.Inactive = false;

            var dto = this._mapper.Map<TbUser, UserDto>(entity);
            await this.Update(dto);

            return dto;
        }

        public async Task<UserDto> GetHost(object id)
        {
            Expression<Func<TbUser, bool>> predicate = null;

            if(long.TryParse(id.ToString(), out long longId))
            { 
                predicate = (TbUser u) => u.Host.Id.Equals(longId);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                predicate = (TbUser u) => u.Host.UniqueId.Equals(guid.ToString());
            }
            else throw new NotImplementedException();

            var entity = await this._repo.FindSingleOrDefaultAsync(predicate, this._includes);

            if(entity == null) return null;

            return this._mapper.Map<TbUser, UserDto>(entity);
        }

        public async Task<IEnumerable<HostSummaryDto>> GetHostSummariesByFilter(HostFilterConfigDto filter)
        {
            Expression<Func<TbUser, bool>> predicate =
                (user) => 
                    // Foci...
                    (!filter.Foci.Any() || user.Host.TbFocusHostMappings.Any(mapping => filter.Foci.Contains(mapping.FocusId))) && 
                    !user.Inactive && 
                    !user.Host.Inactive &&
                    // Elastic...
                    (
                        (filter.Elastic == null || filter.Elastic == string.Empty) || 
                        (
                            user.Name.Contains(filter.Elastic) || 
                            user.Surname.Contains(filter.Elastic) || 
                            user.Username.Contains(filter.Elastic)
                        )
                    ) &&
                    // User name...
                    (
                        (filter.Name == null || filter.Name == string.Empty) || 
                        (
                            user.Name.Contains(filter.Name) || 
                            user.Surname.Contains(filter.Name) || 
                            user.Surname.Contains(filter.Name)
                        )
                    );
            Expression<Func<TbUser, object>> orderByFunc = null; 
            Expression<Func<TbUser, object>> orderByDescFunc = (TbUser u) => u.CreationDateUtc;
            var take = filter.Take ?? 10;
            
            var summaries = this._mapper.Map<IEnumerable<TbUser>, IEnumerable<HostSummaryDto>>( 
                await this._repo.FindToListAsync(predicate, this._includes, orderByFunc, orderByDescFunc, take));
            
            return summaries;
        }

        public async Task<UserDto> AddUser(UserDto dto) 
            => await this.Add(dto);

        public async Task UpdateUser(UserDto dto) 
            => await this.Update(dto);

        public async Task DeleteUser(object id) 
            => await this.Delete(id);
    }
}