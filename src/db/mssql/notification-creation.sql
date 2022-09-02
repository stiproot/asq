use ASQ;

-- what types of messages do we want to display?

-- welcome to asq? 
-- someone signed up for your meeting
-- meeting reminder (intervals: 1 hour before start)
-- meeting recording available

-- CREATE TABLE [dbo].[tb_NotificationType](
-- 	[Id] [SMALLINT] NOT NULL PRIMARY KEY,
-- 	[UniqueId] [uniqueidentifier] NOT NULL,
-- 	[CreationDateUtc] datetime not null,
-- 	[CreationUserId] bigint not null,
--     Inactive bit not null default 0,
-- 	[Description] [varchar](25) NOT NULL
-- )
-- GO

create table [dbo].[tb_Notification](
    [Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [CreationDateUtc] datetime not null,
    [CreationUserId] bigint not null,
    [Inactive] bit not null DEFAULT 0,
    UserId [bigint] not null foreign key REFERENCES tb_User(Id),
    Seen [bit] not null default 0,
    Title [varchar](50) not NULL,
    [Message] [varchar](500) not NULL,
    ImgUrl [varchar](150) null,
    VideoUrl [varchar](150) null,
    MeetingUrl [varchar](150) null,
    ExtMeetingUrl [varchar](650) null,
    NotificationTypeId [smallint] not null foreign key references tb_NotificationType(Id),
)

GO