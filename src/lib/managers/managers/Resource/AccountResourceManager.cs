using System.Threading.Tasks;
using DTO.Tracking;
using dbaccess.Repository;
using System;

namespace managers.Resource
{
    public class AccountResourceManager : IAccountResourceManager
    {
        private IUserResourceAccess _userResourceAccess;
        private IAccountCreationTrackingResourceAccess _accountCreationTrackingResourceAccess;
        private IAccountUpdateTrackingResourceAccess _accountUpdateTrackingResourceAccess;

        public AccountResourceManager(
            IUserResourceAccess userResourceAccess, 
            IAccountCreationTrackingResourceAccess accountCreationTrackingResourceAccess,
            IAccountUpdateTrackingResourceAccess accountUpdateTrackingResourceAccess)
        {
            this._userResourceAccess = userResourceAccess ?? throw new ArgumentNullException(nameof(userResourceAccess));
            this._accountCreationTrackingResourceAccess = accountCreationTrackingResourceAccess ?? throw new ArgumentNullException(nameof(accountCreationTrackingResourceAccess));
            this._accountUpdateTrackingResourceAccess = accountUpdateTrackingResourceAccess ?? throw new ArgumentNullException(nameof(accountUpdateTrackingResourceAccess));
        }

        public async Task<AccountCreationTrackingDto> GetAccountCreationTracking(Guid id)
        {
            return await _accountCreationTrackingResourceAccess.GetAccountCreationTracking(id);
        }
        
        public async Task AddAccountCreationTracking(AccountCreationTrackingDto dto)
        {
            await _accountCreationTrackingResourceAccess.AddAccountCreationTracking(dto);
        }

        public async Task UpdateAccountCreationTracking(AccountCreationTrackingDto dto)
        {
            await _accountCreationTrackingResourceAccess.UpdateAccountCreationTracking(dto);
        }

        public async Task<AccountUpdateTrackingDto> GetAccountUpdateTracking(Guid id)
        {
            return await _accountUpdateTrackingResourceAccess.GetAccountUpdateTracking(id);
        }
        
        public async Task AddAccountUpdateTracking(AccountUpdateTrackingDto dto)
        {
            await _accountUpdateTrackingResourceAccess.AddAccountUpdateTracking(dto);
        }

        public async Task UpdateAccountUpdateTracking(AccountUpdateTrackingDto dto)
        {
            await _accountUpdateTrackingResourceAccess.UpdateAccountUpdateTracking(dto);
        }
    }
}