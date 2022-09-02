use ASQ;

create table [dbo].[tb_BlogPost](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	[Title] [varchar](50) NOT NULL,
	[Content] [varchar](MAX) NOT NULL,
	[ImgId] bigint not null FOREIGN key REFERENCES tb_Img(Id),
	[UserId] BIGINT not null FOREIGN key REFERENCES tb_User(Id),
)
GO

CREATE TABLE [dbo].[tb_FocusBlogPostMapping](
	[Id] [BIGINT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UniqueId] [uniqueidentifier] NOT NULL,
	[CreationDateUtc] datetime not null,
	[CreationUserId] bigint not null,
    Inactive bit not null default 0,
	BlogPostId BIGINT not null FOREIGN key REFERENCES tb_BlogPost(Id) on delete cascade,
	FocusId SMALLINT not null FOREIGN key REFERENCES tb_Focus(Id),
	CONSTRAINT UC_BLOGPOST_FOCUS_MAPPING UNIQUE (BlogPostId, FocusId)
)
GO