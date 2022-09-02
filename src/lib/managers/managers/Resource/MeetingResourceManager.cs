using managers.Enricher;
using DTO.Domain;
using DTO.Tracking;
using DTO.Domain.Ext.Zoom;
using dbaccess.Repository;
using dbaccess.Repository.QueryEnrichment;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace managers.Resource
{
    public class MeetingResourceManager : IMeetingResourceManager
    {
        private readonly IMeetingResourceAccess _meetingResourceAccess;
        private readonly IMeetingUserMappingResourceAccess _meetingUserMappingResourceAccess;
        private readonly IMeetingCreationTrackingResourceAccess _createMeetingTracking;
        private readonly IMeetingUpdateTrackingResourceAccess _updateMeetingTracking;
        private readonly IMeetingRecordingDownloadTrackingResourceAccess _meetingRecordingDownloadTrackingResourceAccess;
        private readonly IExtZoomMeetingRecordingResourceAccess _extZoomMeetingRecordingResourceAccess;
        private readonly IExtZoomMeetingWebHookResourceAccess _extZoomMeetingWebHookResourceAccess;
        private readonly IMeetingQueryEnrichmentResourceAccess _meetingQueryEnrichmentResourceAccess;


        public MeetingResourceManager(
            IMeetingResourceAccess userResourceAccess, 
            IMeetingUserMappingResourceAccess meetingUserMappingResourceAccess,
            IMeetingCreationTrackingResourceAccess createMeetingTracking,
            IMeetingUpdateTrackingResourceAccess updateMeetingTracking,
            IMeetingRecordingDownloadTrackingResourceAccess meetingRecordingDownloadTrackingResourceAccess,
            IExtZoomMeetingRecordingResourceAccess extZoomMeetingRecordingResourceAccess,
            IExtZoomMeetingWebHookResourceAccess extZoomMeetingWebHookResourceAccess,
            IMeetingQueryEnrichmentResourceAccess meetingQueryEnrichmentResourceAccess
        )
        {
            this._meetingResourceAccess = userResourceAccess ?? throw new ArgumentNullException(nameof(userResourceAccess));
            this._meetingUserMappingResourceAccess = meetingUserMappingResourceAccess ?? throw new ArgumentNullException(nameof(meetingUserMappingResourceAccess));
            this._createMeetingTracking = createMeetingTracking ?? throw new ArgumentNullException(nameof(createMeetingTracking));
            this._updateMeetingTracking = updateMeetingTracking ?? throw new ArgumentNullException(nameof(updateMeetingTracking));
            this._meetingRecordingDownloadTrackingResourceAccess = meetingRecordingDownloadTrackingResourceAccess ?? throw new ArgumentNullException(nameof(meetingRecordingDownloadTrackingResourceAccess));
            this._extZoomMeetingRecordingResourceAccess = extZoomMeetingRecordingResourceAccess ?? throw new ArgumentNullException(nameof(extZoomMeetingRecordingResourceAccess));
            this._extZoomMeetingWebHookResourceAccess = extZoomMeetingWebHookResourceAccess ?? throw new ArgumentNullException(nameof(extZoomMeetingWebHookResourceAccess));
            this._meetingQueryEnrichmentResourceAccess = meetingQueryEnrichmentResourceAccess ?? throw new ArgumentNullException(nameof(meetingQueryEnrichmentResourceAccess));
        }

        // meeting
        //public async Task<MeetingDto> GetMeeting(object id) 
            //=> await _meetingResourceAccess.GetMeeting(id);

        public async Task<MeetingDto> GetMeeting(
            object id, 
            IEnumerable<(string, Func<MeetingDto, object>)> enrichmentRules = null
        ) 
        {
            var meeting = await _meetingResourceAccess.GetMeeting(id);
            if(enrichmentRules != null)
            {
                ObjectEnricher.EnrichObject<MeetingDto>(ref meeting, enrichmentRules);
            }
            return meeting;
        }

        public async Task<IEnumerable<MeetingQueryDto>> BuildMeetingSummaryQueries(MeetingSummaryQueryBuilderConfigDto config) 
            => await this._meetingQueryEnrichmentResourceAccess.BuildMeetingSummaryQueries(config);

        public async Task<IEnumerable<MeetingSummaryDto>> GetMeetingSummariesByFilter(MeetingFilterConfigDto config) 
            => await this._meetingResourceAccess.GetMeetingSummariesByFilter(config);

        public async Task AddMeeting(MeetingDto dto) 
            => await _meetingResourceAccess.AddMeeting(dto);

        public async Task UpdateMeeting(MeetingDto dto) 
            => await _meetingResourceAccess.UpdateMeeting(dto);

        public async Task UpdateMeetings(IEnumerable<MeetingDto> dtos) 
        
            => await _meetingResourceAccess.UpdateMeetings(dtos);

        public async Task<MeetingDto> GetMeetingByExtMeetingId(long extId) 
            => await this._meetingResourceAccess.GetMeetingByExtMeetingId(extId);

        public async Task UpdateMeetingStatus(object id)
            => await this._meetingResourceAccess.UpdateMeetingStatus(id);

        public async Task<IEnumerable<MeetingDto>> GetCompleteMeetingsAndMarkAsRecordingDownloading()
            => await this._meetingResourceAccess.GetCompleteMeetingsAndMarkAsRecordingDownloading();

        // meeting-user-mapping
        public async Task AddMeetingUserMapping(long meetingId, long userId)
            => await this._meetingUserMappingResourceAccess.AddMeetingUserMapping(meetingId, userId);
        public async Task RemoveMeetingUserMapping(long meetingId, long userId)
            => await this._meetingUserMappingResourceAccess.RemoveMeetingUserMapping(meetingId, userId);

        // meeting-creation tracking
        public async Task<MeetingCreationTrackingDto> GetMeetingCreationTracking(Guid id)
            => await _createMeetingTracking.GetMeetingCreationTracking(id);
        
        public async Task AddMeetingCreationTracking(MeetingCreationTrackingDto dto) 
            => await _createMeetingTracking.AddMeetingCreationTracking(dto);

        public async Task UpdateMeetingCreationTracking(MeetingCreationTrackingDto dto) 
            => await _createMeetingTracking.UpdateMeetingCreationTracking(dto);

        // meetin-update tracking
        public async Task<MeetingUpdateTrackingDto> GetMeetingUpdateTracking(Guid id) 
            => await _updateMeetingTracking.GetMeetingUpdateTracking(id);

        public async Task AddMeetingUpdateTracking(MeetingUpdateTrackingDto dto)
            => await _updateMeetingTracking.AddMeetingUpdateTracking(dto);

        public async Task UpdateMeetingUpdateTracking(MeetingUpdateTrackingDto dto)
            => await _updateMeetingTracking.UpdateMeetingUpdateTracking(dto);

        // meeting-recording tracking
        public async Task<MeetingRecordingDownloadTrackingDto> GetMeetingRecordingDownloadTracking(Guid id) 
            => await this._meetingRecordingDownloadTrackingResourceAccess.GetMeetingRecordingDownloadTracking(id);

        public async Task AddMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto)
            => await this._meetingRecordingDownloadTrackingResourceAccess.AddMeetingRecordingDownloadTracking(dto);

        public async Task UpdateMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto)
            => await this._meetingRecordingDownloadTrackingResourceAccess.UpdateMeetingRecordingDownloadTracking(dto);

        // ext-meeting-recording
        public async Task<ExtZoomMeetingRecordingDto> GetExtMeetingRecording(Guid id) 
            => await this._extZoomMeetingRecordingResourceAccess.GetExtZoomMeetingRecording(id);

        public async Task<ExtZoomMeetingRecordingDto> AddExtMeetingRecording(ExtZoomMeetingRecordingDto dto)
            => await this._extZoomMeetingRecordingResourceAccess.AddExtZoomMeetingRecording(dto);

        // meeting web-hook
        public async Task AddExtZoomMeetingWebHook(ExtZoomMeetingWebHookDto dto)
            => await this._extZoomMeetingWebHookResourceAccess.AddExtZoomMeetingWebHook(dto);

        public async Task<ExtZoomMeetingWebHookDto> GetExtZoomMeetingWebHook(Guid id)
            => await this._extZoomMeetingWebHookResourceAccess.GetExtZoomMeetingWebHook(id);
    }
}