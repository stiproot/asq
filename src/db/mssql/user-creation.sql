use ASQ;

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create table [dbo].[tb_AccountCreationTracking](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Request] [varchar](MAX) NOT NULL,
	[Tracking] [varchar](MAX) NOT NULL,
    Failed [bit] not null default 0
)
GO

create table [dbo].[tb_AccountUpdateTracking](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Request] [varchar](MAX) NOT NULL,
	[Tracking] [varchar](MAX) NOT NULL,
    Failed [bit] not null default 0
)
GO

CREATE TABLE [dbo].[tb_Img](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null, 
	[Inactive] bit not null DEFAULT 0,
	[Data] varbinary(max) not null,
	[FileName] varchar(50) not null,
	[Path] varchar(500) not null,
	[ThumbnailUrl] varchar(500) not null,
)
GO

CREATE TABLE [dbo].[tb_AccountType](
	[Id] [SMALLINT] NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Description] [varchar](25) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_CardType](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Description] [varchar](25) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_Focus](
	[Id] [SMALLINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Description] [varchar](25) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_ext_ZoomUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Payload] [varchar](MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_Host](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
    IsConsultant bit null default 0,
	[Company] [varchar](50) NOT NULL,
	[CareerSummary] [varchar] (MAX) NOT NULL,
	ExtUserId BIGINT not null FOREIGN key REFERENCES tb_ext_ZoomUser(Id),
)
GO

CREATE TABLE [dbo].[tb_PaymentInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[CardNumber] [varchar] (20) NOT NULL UNIQUE,
	[ExpirationDate] date NOT NULL,
	[Cvc] varchar(10) not null,
	CardTypeId INT not null FOREIGN key REFERENCES tb_CardType(Id),
)
GO

CREATE TABLE [dbo].[tb_Timezone](
	[Id] [smallint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Display] [varchar](50) NOT NULL UNIQUE,
	[UtcOffset] [smallint] NOT NULL,
	[ExtCode] [varchar](50) NOT NULL UNIQUE,
	CONSTRAINT UC_DISPLAY_EXTCODE UNIQUE (Display, ExtCode)
)
GO

CREATE TABLE [dbo].[tb_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Username] [varchar](25) NOT NULL UNIQUE,
	[Name] [varchar] (25) NOT NULL,
	[Surname] [varchar] (25) NOT NULL,
	[Email] [varchar] (50) NOT NULL UNIQUE,
	[Password] [varchar] (25) NOT NULL,
	[AccountTypeId] SMALLINT not null FOREIGN key REFERENCES tb_AccountType(Id),
	[ImgId] bigint not null FOREIGN key REFERENCES tb_Img(Id),
	HostId bigint null FOREIGN KEY REFERENCES tb_Host(Id),
    TimezoneId smallint not null FOREIGN key REFERENCES tb_Timezone(Id),
	[PaymentInfoId] BIGINT not null FOREIGN key REFERENCES tb_PaymentInfo(Id),
)
GO

CREATE TABLE [dbo].[tb_FocusUserMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id) on delete cascade,
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
	CONSTRAINT UC_USER_FOCUS_MAPPING UNIQUE (UserId, FocusId)
)
GO

CREATE TABLE [dbo].[tb_FocusHostMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	HostId BIGINT not null FOREIGN key REFERENCES tb_Host(Id) on delete cascade,
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
	CONSTRAINT UC_HOST_FOCUS_MAPPING UNIQUE (HostId, FocusId)
)
GO


