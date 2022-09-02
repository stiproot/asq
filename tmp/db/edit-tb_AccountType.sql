use ASQ;

--select * from sys.foreign_keys

--alter table tb_User drop CONSTRAINT FK__tb_User__Account__6EF57B66

--drop table tb_AccountType

--CREATE TABLE [dbo].[tb_AccountType](
	--[Id] [SMALLINT] NOT NULL PRIMARY KEY,
	--[UniqueId] [uniqueidentifier] NOT NULL,
	--[Description] [varchar](25) NOT NULL
--)

--ALTER TABLE tb_User ADD FOREIGN KEY (AccountTypeId) REFERENCES tb_AccountType(Id);

--insert into tb_AccountType values (0, NEWID(), 'Student')
--insert into tb_AccountType values (1, NEWID(), 'Host')

--select * from tb_AccountType

--alter table tb_Host drop CONSTRAINT FK__tb_Host__UserId__74AE54BC
alter table tb_Host add FOREIGN key (UserId) references tb_User(Id)
