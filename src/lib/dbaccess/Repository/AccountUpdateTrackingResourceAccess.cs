using System;
using System.Threading.Tasks;
using dbaccess.Factory;
using dbaccess.Models;
using DTO.Tracking;
using AutoMapper;
using  System.Linq.Expressions;

namespace dbaccess.Repository
{
    public class AccountUpdateTrackingResourceAccess: BaseResourceAccess<AccountUpdateTrackingDto, TbAccountUpdateTracking>, IAccountUpdateTrackingResourceAccess
    {
        public AccountUpdateTrackingResourceAccess(IGenericRepositoryFactory repositoryFactory, IMapper mapper): base(repositoryFactory, mapper){ }

        public async Task<AccountUpdateTrackingDto> GetAccountUpdateTracking(object id)
        {
            TbAccountUpdateTracking entity;
            if(long.TryParse(id.ToString(), out long longId))
            {
                entity = await this._repo.FindByKey(id);
            }
            else if(Guid.TryParse(id.ToString(), out Guid guid))
            {
                Expression<Func<TbAccountUpdateTracking, bool>> predicate = (TbAccountUpdateTracking u) => u.UniqueId.Equals(id);
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
            
            return this._mapper.Map<TbAccountUpdateTracking, AccountUpdateTrackingDto>(entity);
        }

        public async Task<AccountUpdateTrackingDto> AddAccountUpdateTracking(AccountUpdateTrackingDto dto) 
            => await this.Add(dto);

        public async Task UpdateAccountUpdateTracking(AccountUpdateTrackingDto dto)
            => await this.Update(dto);
    }
}