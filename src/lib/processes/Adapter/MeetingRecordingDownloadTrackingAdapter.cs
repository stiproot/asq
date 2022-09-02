using DTO.Tracking;
using managers.Resource;
using System.Threading.Tasks;
using System;

namespace processes.Adapter
{
    public class MeetingRecordingDownloadTrackingAdapter: ITrackingAdapter
    {
        private readonly IMeetingResourceManager _resourceManager;

        public MeetingRecordingDownloadTrackingAdapter(IMeetingResourceManager resourceManager) => this._resourceManager = resourceManager; 

        public async Task<BaseTracking> Get(Guid id) => await this._resourceManager.GetMeetingRecordingDownloadTracking(id);
        public async Task Update(BaseTracking tracking) 
            => await this._resourceManager.UpdateMeetingRecordingDownloadTracking((MeetingRecordingDownloadTrackingDto)tracking);
    }
}