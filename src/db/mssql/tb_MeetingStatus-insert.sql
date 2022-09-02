use ASQ;

-- select * from tb_MeetingStatus

SET IDENTITY_INSERT [tb_MeetingStatus] ON;

-- insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description]) values (1, NEWID(), GETDATE(), 0, 0, 'Awaiting');

-- insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description]) values (2, NEWID(), GETDATE(), 0, 0, 'Cancelled');

-- insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description]) values (3, NEWID(), GETDATE(), 0, 0, 'Complete');

-- insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description]) values (4, NEWID(), GETDATE(), 0, 0, 'Complete Recording Download In Progress');

-- insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description]) values (5, NEWID(), GETDATE(), 0, 0, 'Complete With Recordings');


SET IDENTITY_INSERT [tb_MeetingStatus] OFF;
