SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_UpdateMeetingStatus]
    @meetingId bigint
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    -- check to total number of participants joined the meeting

    select *
    from tb_MeetingUserMapping mapping
        inner join tb_User u on mapping.UserId = u.Id
    where mapping.MeetingId = @meetingId

    -- select top 1 [m].* 
    -- from [dbo].[tb_Meeting] [m] (nolock)
    --     inner join [dbo].tb_ext_ZoomMeeting [ext] (nolock)
    --         on [m].ExtMeetingId = [ext].[Id]
    -- where JSON_VALUE([ext].Payload, '$.id') = @extId 
END

GO
