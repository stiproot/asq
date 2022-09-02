use ASQ;

insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (1, UUID(), CURDATE(), 0, 0, 'Awaiting');
insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (2, UUID(), CURDATE(), 0, 0, 'Cancelled');
insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (3, UUID(), CURDATE(), 0, 0, 'Complete');
insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (4, UUID(), CURDATE(), 0, 0, 'Complete Recording Download In Progress');
insert into tb_MeetingStatus(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, Description) values (5, UUID(), CURDATE(), 0, 0, 'Complete With Recordings');