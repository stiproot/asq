use ASQ;

SET IDENTITY_INSERT [tb_Timezone] ON;
insert into tb_Timezone(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], [UtcOffset], [ExtCode]) values (1, NEWID(), GETDATE(), 0, 0, 'Midway Island, Samoa', 14, 'Pacific/Midway');
insert into tb_Timezone(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], [UtcOffset], [ExtCode]) values (3, NEWID(), GETDATE(), 0, 0, 'Hawaii', 10, 'Pacific/Honolulu');
insert into tb_Timezone(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], [UtcOffset], [ExtCode]) values (5, NEWID(), GETDATE(), 0, 0, 'Vancouver', -7, 'America/Vancouver');
insert into tb_Timezone(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], [UtcOffset], [ExtCode]) values (66, NEWID(), GETDATE(), 0, 0, 'Nairobi', 3, 'Africa/Nairobi');
insert into tb_Timezone(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], [UtcOffset], [ExtCode]) values (81, NEWID(), GETDATE(), 0, 0, 'Johannesburg', 2, 'Africa/Johannesburg');
SET IDENTITY_INSERT [tb_Timezone] OFF;
