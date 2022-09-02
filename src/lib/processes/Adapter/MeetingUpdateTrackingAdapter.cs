using DTO.Tracking;
using managers.Resource;
using System.Threading.Tasks;
using System;

namespace processes.Adapter
{
    public class MeetingUpdateTrackingAdapter: ITrackingAdapter
    {
        private readonly IMeetingResourceManager _resourceManager;

        public MeetingUpdateTrackingAdapter(IMeetingResourceManager resourceManager) => this._resourceManager = resourceManager; 

        public async Task<BaseTracking> Get(Guid id) => await this._resourceManager.GetMeetingUpdateTracking(id);
        public async Task Update(BaseTracking tracking) 
            => await this._resourceManager.UpdateMeetingUpdateTracking((MeetingUpdateTrackingDto)tracking);
    }
}