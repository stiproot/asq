using System.Threading.Tasks;
using DTO.Tracking;
using System;

namespace managers.Resource
{
    public interface IAccountResourceManager
    {
        Task<AccountCreationTrackingDto> GetAccountCreationTracking(Guid id);
        Task AddAccountCreationTracking(AccountCreationTrackingDto dto);
        Task UpdateAccountCreationTracking(AccountCreationTrackingDto dto);
        Task<AccountUpdateTrackingDto> GetAccountUpdateTracking(Guid id);
        Task AddAccountUpdateTracking(AccountUpdateTrackingDto dto);
        Task UpdateAccountUpdateTracking(AccountUpdateTrackingDto dto);
    }
}