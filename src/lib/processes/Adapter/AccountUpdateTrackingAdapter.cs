using System.Threading.Tasks;
using DTO.Tracking;
using managers.Resource;
using System;

namespace processes.Adapter
{
    public class AccountUpdateTrackingAdapter: ITrackingAdapter
    {
        private readonly IAccountResourceManager _resourceManager;

        public AccountUpdateTrackingAdapter(IAccountResourceManager resourceManager) => this._resourceManager = resourceManager; 

        public async Task<BaseTracking> Get(Guid id) => await this._resourceManager.GetAccountUpdateTracking(id);
        public async Task Update(BaseTracking tracking) 
            => await this._resourceManager.UpdateAccountUpdateTracking((AccountUpdateTrackingDto)tracking);
    }
}