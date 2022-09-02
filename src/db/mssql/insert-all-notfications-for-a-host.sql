use ASQ;

INSERT INTO tb_Notification (UniqueId, CreationDateUtc, CreationUserId, Inactive, [UserId], [Seen], [Title], [Message], [ImgUrl], [VideoUrl], [MeetingUrl], [ExtMeetingUrl], [NotificationTypeId])
SELECT
    NEWID(), 
    GETDATE(), 
    0, 
    0, 
    2, 
    0, 
    'Meeting Successfully Created', 
    m.[Title], 
    i.ThumbnailUrl,
    '',
    'htttp://localhost:4200/meeting/' + cast(m.UniqueId as varchar(650)),
    JSON_VALUE([ext].Payload, '$.start_url'),
    0
from tb_Meeting m
    inner join tb_Img i on m.ImgId = i.Id
    inner join tb_ext_ZoomMeeting ext on m.ExtMeetingId = ext.Id
WHERE m.HostId = 2


-- select *
-- from tb_Meeting m
--     inner join tb_Img i on m.ImgId = i.Id
--     inner join tb_ext_ZoomMeeting ext on m.ExtMeetingId = ext.Id
