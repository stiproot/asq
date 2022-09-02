use ASQ;

insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description)
values (0, UUID(), CURDATE(), 0, 0, 'MEETING_CREATED');

insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description)
values (1, UUID(), CURDATE(), 0, 0, 'MEETING_PARTICIPATION');

insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description)
values (2, UUID(), CURDATE(), 0, 0, 'MEETING_REMINDER');