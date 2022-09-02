use ASQ;

create table [dbo].[tb_MailTracking](
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

-- awaiting = 0
-- processing = 1
-- sent = 2