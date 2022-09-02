use ASQ;

insert into tb_AccountType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (0, NEWID(), GETDATE(), 0, 0, 'STUDENT')

insert into tb_AccountType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (1, NEWID(), GETDATE(), 0, 0, 'HOST')