using System.Threading.Tasks;
using DTO.Tracking;

namespace dbaccess.Repository
{
    public interface IAccountUpdateTrackingResourceAccess
    {
        Task<AccountUpdateTrackingDto> GetAccountUpdateTracking(object id);
        Task<AccountUpdateTrackingDto> AddAccountUpdateTracking(AccountUpdateTrackingDto dto);
        Task UpdateAccountUpdateTracking(AccountUpdateTrackingDto dto);
    }
}