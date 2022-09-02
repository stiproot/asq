use ASQ;

insert into tb_CardType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (1, UUID(), CURDATE(), 0, 0, 'Visa');
insert into tb_CardType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (2, UUID(), CURDATE(), 0, 0, 'MasterCard');
insert into tb_CardType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (3, UUID(), CURDATE(), 0, 0, 'AMEX');