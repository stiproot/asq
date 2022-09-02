using System.Threading.Tasks;
using DTO.Tracking;

namespace dbaccess.Repository
{
    public interface IAccountCreationTrackingResourceAccess
    {
        Task<AccountCreationTrackingDto> GetAccountCreationTracking(object id);
        Task<AccountCreationTrackingDto> AddAccountCreationTracking(AccountCreationTrackingDto dto);
        Task UpdateAccountCreationTracking(AccountCreationTrackingDto dto);
    }
}