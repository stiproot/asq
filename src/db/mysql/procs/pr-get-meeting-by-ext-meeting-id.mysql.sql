use ASQ;

delimiter //

create PROCEDURE pr_GetMeetingByExtMeetingId 
(
    in extId bigint
)
BEGIN
    select
        m.Id,
        m.UniqueId,
        m.CreationDateUtc,
        m.CreationUserId,
        m.Inactive,
        m.Title,
        m.Description,
        m.StartDateUtc,
        m.EstimatedDuration,
        m.HostId,
        m.MeetingStatusId,
        m.ImgId,
        m.ExtMeetingId,
        m.TimezoneId
    from tb_Meeting m
    inner join tb_ext_ZoomMeeting ext
        on m.ExtMeetingId = ext.Id
    where JSON_CONTAINS(ext.Payload, cast(extId as char(100)), '$.id')
    limit 1;
END //
