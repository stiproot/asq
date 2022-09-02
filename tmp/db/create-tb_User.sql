use ASQ;

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tb_AccountType](
	[Id] [SMALLINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Description] [varchar](25) NOT NULL
)

CREATE TABLE [dbo].[tb_Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Description] [varchar](25) NOT NULL
)

CREATE TABLE [dbo].[tb_Focus](
	[Id] [SMALLINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Description] [varchar](25) NOT NULL
)

CREATE TABLE [dbo].[tb_User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](25) NOT NULL,
	[Name] [varchar] (50) NOT NULL,
	[Surname] [varchar] (50) NOT NULL,
	[Email] [varchar] (50) NOT NULL,
	[Password] [varchar] (50) NOT NULL,
	[CreationDateUTC] datetime not null
	AccountTypeId SMALLINT not null FOREIGN key REFERENCES tb_AccountType(Id)
)

CREATE TABLE [dbo].[tb_ext_ZoomUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id)
)

CREATE TABLE [dbo].[tb_Host](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[Company] [varchar](50) NOT NULL,
	[CareerSummary] [varchar] (MAX) NOT NULL,
	[CreationDateUTC] datetime not null
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id)
)

CREATE TABLE [dbo].[tb_FocusUserMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	UserId BIGINT not null FOREIGN key REFERENCES tb_User(Id),
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
    isDisabled bit not null default 0
)

CREATE TABLE [dbo].[tb_FocusHostMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	HostId BIGINT not null FOREIGN key REFERENCES tb_Host(Id),
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
    isDisabled bit not null default 0
)

CREATE TABLE [dbo].[tb_BankingInfo](
	[Id] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[AccountNumber] [varchar] (25) NOT NULL,
	[BranchCode] [varchar] (10) NOT NULL,
	[CreationDateUTC] datetime not null,
	BankId INT not null FOREIGN key REFERENCES tb_Bank(Id),
	HostId BIGINT not null FOREIGN key REFERENCES tb_Host(Id)
)

