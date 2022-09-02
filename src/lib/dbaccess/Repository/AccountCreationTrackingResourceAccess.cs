using dbaccess.Factory;
using dbaccess.Models;
using DTO.Tracking;
using AutoMapper;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;

namespace dbaccess.Repository
{
    public class AccountCreationTrackingResourceAccess: BaseResourceAccess<AccountCreationTrackingDto, TbAccountCreationTracking>, IAccountCreationTrackingResourceAccess
    {
        public AccountCreationTrackingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<AccountCreationTrackingDto> GetAccountCreationTracking(object id)
        {
            TbAccountCreationTracking entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbAccountCreationTracking, bool>> predicate = (TbAccountCreationTracking u) => u.UniqueId.Equals(id);
                entity = await this._repo.FindSingleOrDefaultAsync(predicate, null);
            }
            else
            {
                entity = null;
            }

            if(entity == null)
            {
                return null;
            }
            
            return this._mapper.Map<TbAccountCreationTracking, AccountCreationTrackingDto>(entity);
        }

        public async Task<AccountCreationTrackingDto> AddAccountCreationTracking(AccountCreationTrackingDto dto) 
            => await this.Add(dto);

        public async Task UpdateAccountCreationTracking(AccountCreationTrackingDto dto) 
            => await this.Update(dto);
    }
}