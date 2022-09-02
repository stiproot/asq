use ASQ;

--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--CREATE TABLE [dbo].[tb_AccountCreationTracking](
	--[AccountCreationTrackingId] [uniqueidentifier] NOT NULL,
	--[Request] [varchar](MAX) NOT NULL,
	--[Tracking] [varchar](MAX) NOT NULL,
	--[Failed] [bit] NOT NULL,
	--[CreationDateUTC] datetime NOT NULL
--)
--GO

select * from tb_AccountCreationTracking

--insert into tb_AccountCreationTracking
--values ('0786a1c7-29a1-4e5c-b08e-f6574ea1e214', '', '[{"identifier":"persist-user","failed":null,"response":null,"exception_info":null}]', 0, GETDATE())