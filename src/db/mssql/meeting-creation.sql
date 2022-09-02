use ASQ;

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create table [dbo].[tb_MeetingRecordingDownloadTracking](
    [Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
    [CreationDateUtc] datetime not null,
    [CreationUserId] bigint not null,
    [Inactive] bit not null DEFAULT 0,
    [Request] [varchar](MAX) NOT NULL,
    [Tracking] [varchar](MAX) NOT NULL,
    Failed [bit] not null default 0,
    StatusId [smallint] not null DEFAULT 0
)
GO

create table [dbo].[tb_MeetingUpdateTracking](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
	[Inactive] bit not null DEFAULT 0,
	[Request] [varchar](MAX) NOT NULL,
	[Tracking] [varchar](MAX) NOT NULL,
 	Failed [bit] not null default 0,
)
GO

create table [dbo].[tb_MeetingCreationTracking](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
	[Inactive] bit not null DEFAULT 0,
	[Request] [varchar](MAX) NOT NULL,
	[Tracking] [varchar](MAX) NOT NULL,
    Failed [bit] not null default 0
)
GO

CREATE TABLE [dbo].[tb_MeetingStatus](
	[Id] [SMALLINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null, 
	[Inactive] bit not null DEFAULT 0,
	[Description] [varchar](50) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_ext_ZoomMeeting](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Payload] [varchar](MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_Meeting](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null FOREIGN KEY REFERENCES tb_User(Id),
	[Inactive] bit not null DEFAULT 0,
	[Title] [varchar](50) NOT NULL UNIQUE,
	[Description] [varchar] (500) NOT NULL,
	[StartDateUtc] datetime NOT NULL,
	[EstimatedDuration] int NOT NULL,
	[HostId] bigint NOT NULL FOREIGN key REFERENCES tb_Host(Id),
	[MeetingStatusId] SMALLINT not null FOREIGN key REFERENCES tb_MeetingStatus(Id),
	[ImgId] bigint not null FOREIGN key REFERENCES tb_Img(Id),
	ExtMeetingId BIGINT not null FOREIGN key REFERENCES tb_ext_ZoomMeeting(Id),
	TimezoneId smallint not null FOREIGN key REFERENCES tb_Timezone(Id),
)
GO

CREATE TABLE [dbo].[tb_ext_ZoomMeetingRecording](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Payload] [varchar](MAX) NOT NULL,
	ExtMeetingId bigint not null FOREIGN key REFERENCES tb_ext_ZoomMeeting(Id)
)
GO

CREATE TABLE [dbo].[tb_MeetingRecording](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null, 
	[Inactive] bit not null DEFAULT 0,
	[FileName] varchar(50) not null,
	[Path] varchar(500) not null,
    [Part] smallint not null,
    MeetingId BIGINT not null FOREIGN key REFERENCES tb_Meeting(Id),
	ExtMeetingRecordingId bigint not null FOREIGN key REFERENCES tb_ext_ZoomMeetingRecording(Id)
)
GO

CREATE TABLE [dbo].[tb_FocusMeetingMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	MeetingId BIGINT not null FOREIGN key REFERENCES tb_Meeting(Id) on delete cascade,
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
	CONSTRAINT UC_MEETING_FOCUS_MAPPING UNIQUE (MeetingId, FocusId)
)
GO

CREATE TABLE [dbo].[tb_ext_ZoomWebHook](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	MeetingId BIGINT not null FOREIGN key REFERENCES tb_Meeting(Id) on delete cascade,
	[Payload] [varchar](MAX) NOT NULL,
	[EventType] [varchar](30) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_MeetingUserMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id) on delete cascade,
	MeetingId bigint not null FOREIGN key REFERENCES tb_Meeting(Id) on delete cascade,
	CONSTRAINT UC_USER_MEETING_MAPPING UNIQUE (UserId, MeetingId)
)
GO

CREATE TABLE [dbo].[tb_MeetingReview](
	[Id] bigint IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
    [MeetingUserMappingId] bigint not null FOREIGN key REFERENCES tb_MeetingUserMapping(Id) on delete cascade,
	[Review] [varchar](500) NOT NULL,
    [Rating] FLOAT not NULL DEFAULT 0
)
GO