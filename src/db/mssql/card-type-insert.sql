use ASQ;

-- select * from tb_CardType

SET IDENTITY_INSERT [tb_CardType] ON;

insert into tb_CardType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (1, NEWID(), GETDATE(), 0, 0, 'Visa')

insert into tb_CardType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (2, NEWID(), GETDATE(), 0, 0, 'MasterCard')

insert into tb_CardType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (3, NEWID(), GETDATE(), 0, 0, 'AMEX')

SET IDENTITY_INSERT [tb_CardType] OFF;