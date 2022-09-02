using System.Threading.Tasks;
using DTO.Tracking;
using managers.Resource;
using System;

namespace processes.Adapter
{
    public class AccountCreationTrackingAdapter: ITrackingAdapter
    {
        private readonly IAccountResourceManager _resourceManager;

        public AccountCreationTrackingAdapter(IAccountResourceManager resourceManager) => this._resourceManager = resourceManager; 

        public async Task<BaseTracking> Get(Guid id) => await this._resourceManager.GetAccountCreationTracking(id);
        public async Task Update(BaseTracking tracking) 
            => await this._resourceManager.UpdateAccountCreationTracking((AccountCreationTrackingDto)tracking);
    }
}