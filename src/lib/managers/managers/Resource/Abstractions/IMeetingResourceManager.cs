using DTO.Domain;
using DTO.Tracking;
using DTO.Domain.Ext.Zoom;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace managers.Resource
{
    public interface IMeetingResourceManager
    {
        //Task<MeetingDto> GetMeeting(object id);
        Task<MeetingDto> GetMeeting(
            object id, 
            IEnumerable<(string, Func<MeetingDto, object>)> enrichmentRules = null);
        Task<IEnumerable<MeetingQueryDto>> BuildMeetingSummaryQueries(MeetingSummaryQueryBuilderConfigDto config);
        Task<IEnumerable<MeetingSummaryDto>> GetMeetingSummariesByFilter(MeetingFilterConfigDto config);
        Task AddMeeting(MeetingDto dto);
        Task UpdateMeeting(MeetingDto dto);
        Task UpdateMeetings(IEnumerable<MeetingDto> dtos);
        Task<MeetingDto> GetMeetingByExtMeetingId(long extId);
        Task UpdateMeetingStatus(object id);
        Task<IEnumerable<MeetingDto>> GetCompleteMeetingsAndMarkAsRecordingDownloading();

        Task AddMeetingUserMapping(long meetingId, long userId);
        Task RemoveMeetingUserMapping(long meetingId, long userId);

        Task<MeetingCreationTrackingDto> GetMeetingCreationTracking(Guid id);
        Task AddMeetingCreationTracking(MeetingCreationTrackingDto dto);
        Task UpdateMeetingCreationTracking(MeetingCreationTrackingDto dto);

        Task<MeetingUpdateTrackingDto> GetMeetingUpdateTracking(Guid id);
        Task AddMeetingUpdateTracking(MeetingUpdateTrackingDto dto);
        Task UpdateMeetingUpdateTracking(MeetingUpdateTrackingDto dto);

        Task<MeetingRecordingDownloadTrackingDto> GetMeetingRecordingDownloadTracking(Guid id);
        Task AddMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto);
        Task UpdateMeetingRecordingDownloadTracking(MeetingRecordingDownloadTrackingDto dto);

        Task<ExtZoomMeetingRecordingDto> GetExtMeetingRecording(Guid id);
        Task<ExtZoomMeetingRecordingDto> AddExtMeetingRecording(ExtZoomMeetingRecordingDto dto);

        Task<ExtZoomMeetingWebHookDto> GetExtZoomMeetingWebHook(Guid id);
        Task AddExtZoomMeetingWebHook(ExtZoomMeetingWebHookDto dto);
    }
}