using DTO.Tracking;
using managers.Resource;
using System.Threading.Tasks;
using System;

namespace processes.Adapter
{
    public class MeetingCreationTrackingAdapter: ITrackingAdapter
    {
        private readonly IMeetingResourceManager _resourceManager;

        public MeetingCreationTrackingAdapter(IMeetingResourceManager resourceManager) => this._resourceManager = resourceManager; 

        public async Task<BaseTracking> Get(Guid id) => await this._resourceManager.GetMeetingCreationTracking(id);
        public async Task Update(BaseTracking tracking) 
            => await this._resourceManager.UpdateMeetingCreationTracking((MeetingCreationTrackingDto)tracking);
    }
}