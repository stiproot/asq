use ASQ;

insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (0, NEWID(), GETDATE(), 0, 0, 'MEETING_CREATED')

insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (1, NEWID(), GETDATE(), 0, 0, 'MEETING_PARTICIPATION')

insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
values (2, NEWID(), GETDATE(), 0, 0, 'MEETING_REMINDER')