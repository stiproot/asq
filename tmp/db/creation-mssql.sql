use ASQ;

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--create table [dbo].[tb_AccountCreationTracking](
	--[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	--[UniqueId] [uniqueidentifier] NOT NULL,
	--[Request] [varchar](MAX) NOT NULL,
	--[Tracking] [varchar](MAX) NOT NULL,
    --Failed [bit] not null default 0,
	--[CreationDateUTC] datetime not null
--)
--GO

--CREATE TABLE [dbo].[tb_AccountType](
	--[Id] [SMALLINT] NOT NULL PRIMARY KEY,
	--[UniqueId] [uniqueidentifier] NOT NULL,
	--[Description] [varchar](25) NOT NULL
--)
--GO

-- desc currently 50
CREATE TABLE [dbo].[tb_CardType](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Description] [varchar](25) NOT NULL
)
--GO

--CREATE TABLE [dbo].[tb_Focus](
	--[Id] [SMALLINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	--[UniqueId] [uniqueidentifier] NOT NULL,
	--[Description] [varchar](25) NOT NULL
--)
--GO

--name, surname, pwd currently have length of 50
CREATE TABLE [dbo].[tb_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](25) NOT NULL UNIQUE,
	[Name] [varchar] (25) NOT NULL,
	[Surname] [varchar] (25) NOT NULL,
	[Email] [varchar] (50) NOT NULL UNIQUE,
	[Password] [varchar] (25) NOT NULL,
	[CreationDateUTC] datetime not null,
	[AccountTypeId] SMALLINT not null FOREIGN key REFERENCES tb_AccountType(Id)
)
GO

CREATE TABLE [dbo].[tb_Host](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Company] [varchar](50) NOT NULL,
	[CareerSummary] [varchar] (MAX) NOT NULL,
	[CreationDateUTC] datetime not null,
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id)
)
GO

CREATE TABLE [dbo].[tb_ext_ZoomUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	HostId BIGINT not null FOREIGN key REFERENCES tb_Host(Id),
	[Payload] [varchar](MAX) NOT NULL
)
GO

CREATE TABLE [dbo].[tb_FocusUserMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id) on delete cascade,
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
    InActive bit not null default 0,
	CONSTRAINT UC_MAPPING_USER UNIQUE (UserId, FocusId)
)
GO

CREATE TABLE [dbo].[tb_FocusHostMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	HostId BIGINT not null FOREIGN key REFERENCES tb_Host(Id) on delete cascade,
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
    InActive bit not null default 0,
	CONSTRAINT UC_MAPPING_HOST UNIQUE (HostId, FocusId)
)
GO

-- ef model missing inactive
CREATE TABLE [dbo].[tb_PaymentInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUTC] datetime not null,
	[UserId] BIGINT not null FOREIGN key REFERENCES tb_User(Id),
	[CardNumber] [varchar] (20) NOT NULL UNIQUE,
	[ExpirationDate] date (10) NOT NULL,
	[CVC] varchar(10) not null,
	CarTypeId INT not null FOREIGN key REFERENCES tb_CardType(Id),
	[InActive] bit not null default 0
)
GO

