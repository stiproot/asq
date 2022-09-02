use ASQ;

create table tb_NotificationTracking(
    Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
    UniqueId char(38) NOT NULL,
    CreationDateUtc datetime not null,
    CreationUserId bigint not null,
    Inactive bit not null,
    Request JSON NOT NULL,
    Tracking JSON NOT NULL,
    Failed bit not null,
    StatusId smallint not null DEFAULT 0
);

create table tb_Mail(
    Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
    UniqueId char(38) NOT NULL,
    CreationDateUtc datetime not null,
    CreationUserId bigint not null,
    Inactive bit not null,
    Subject varchar(900) NOT NULL,
    Body varchar(2500) not null,
    ToEmailAddress varchar(255) not null,
    FromEmailAddress varchar(255) not null,
    StatusId smallint not null DEFAULT 0
);

CREATE TABLE tb_NotificationType(
	Id SMALLINT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Description varchar(25) NOT NULL
);

create table tb_Notification(
    Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
    UniqueId char(38) NOT NULL,
    CreationDateUtc datetime not null,
    CreationUserId bigint not null,
    Inactive bit not null,
    UserId bigint not null,
    foreign key (UserId) REFERENCES tb_User(Id),
    Seen bit not null,
    Title varchar(50) not NULL,
    Message varchar(500) not NULL,
    ImgUrl varchar(150) null,
    VideoUrl varchar(150) null,
    MeetingUrl varchar(150) null,
    ExtMeetingUrl varchar(650) null,
    NotificationTypeId smallint not null,
    foreign key (NotificationTypeId) references tb_NotificationType(Id)
);