use ASQ;
GO
create PROCEDURE [dbo].[pr_GetMeetingByExtMeetingId]
    @extId bigint
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

    select top 1 [m].* 
    from [dbo].[tb_Meeting] [m] (nolock)
        inner join [dbo].tb_ext_ZoomMeeting [ext] (nolock)
            on [m].ExtMeetingId = [ext].[Id]
    where JSON_VALUE([ext].Payload, '$.id') = @extId 
END
