use ASQ;

create table tb_MeetingRecordingDownloadTracking(
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

create table tb_MeetingUpdateTracking(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
	Inactive bit not null,
	Request JSON NOT NULL,
	Tracking JSON NOT NULL,
 	Failed bit not null
);

create table tb_MeetingCreationTracking(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
	Inactive bit not null,
	Request JSON NOT NULL,
	Tracking JSON NOT NULL,
  Failed bit not null
);

CREATE TABLE tb_MeetingStatus(
	Id SMALLINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null, 
	Inactive bit not null,
	Description varchar(50) NOT NULL
);

CREATE TABLE tb_ext_ZoomMeeting(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Payload JSON NOT NULL
);

CREATE TABLE tb_Meeting(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
	FOREIGN KEY (CreationUserId) REFERENCES tb_User(Id),
	Inactive bit not null,
	Title varchar(50) NOT NULL UNIQUE,
	Description varchar (500) NOT NULL,
	StartDateUtc datetime NOT NULL,
	EstimatedDuration int NOT NULL,
	HostId bigint NOT NULL,
	FOREIGN key (HostId) REFERENCES tb_Host(Id),
	MeetingStatusId SMALLINT not null,
	FOREIGN key (MeetingStatusId) REFERENCES tb_MeetingStatus(Id),
	ImgId bigint not null,
	FOREIGN key (ImgId) REFERENCES tb_Img(Id),
	ExtMeetingId BIGINT not null,
	FOREIGN key (ExtMeetingId) REFERENCES tb_ext_ZoomMeeting(Id),
	TimezoneId smallint not null,
	FOREIGN key (TimezoneId) REFERENCES tb_Timezone(Id)
);

CREATE TABLE tb_ext_ZoomMeetingRecording(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Payload JSON NOT NULL,
	ExtMeetingId bigint not null,
	FOREIGN key (ExtMeetingId) REFERENCES tb_ext_ZoomMeeting(Id)
);

CREATE TABLE tb_MeetingRecording(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null, 
	Inactive bit not null,
	FileName varchar(50) not null,
	Path varchar(500) not null,
  Part smallint not null,
  MeetingId BIGINT not null,
	FOREIGN key (MeetingId) REFERENCES tb_Meeting(Id),
	ExtMeetingRecordingId bigint not null,
	FOREIGN key (ExtMeetingRecordingId) REFERENCES tb_ext_ZoomMeetingRecording(Id)
);

CREATE TABLE tb_FocusMeetingMapping(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	MeetingId BIGINT not null,
	FOREIGN key (MeetingId) REFERENCES tb_Meeting(Id) on delete cascade,
	FocusId SMALLINT not null,
	FOREIGN key (FocusId) REFERENCES tb_Focus(Id),
	CONSTRAINT UC_MEETING_FOCUS_MAPPING UNIQUE (MeetingId, FocusId)
);

CREATE TABLE tb_ext_ZoomWebHook(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	MeetingId BIGINT not null,
	FOREIGN key (MeetingId) REFERENCES tb_Meeting(Id) on delete cascade,
	Payload JSON NOT NULL,
	EventType varchar(30) NOT NULL
);

CREATE TABLE tb_MeetingUserMapping(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	UserId BIGINT not null,
	FOREIGN key (UserId) REFERENCES tb_User(Id) on delete cascade,
	MeetingId bigint not null,
	FOREIGN key (MeetingId) REFERENCES tb_Meeting(Id) on delete cascade,
	CONSTRAINT UC_USER_MEETING_MAPPING UNIQUE (UserId, MeetingId)
);

CREATE TABLE tb_MeetingReview(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
  MeetingUserMappingId bigint not null,
	FOREIGN key (MeetingUserMappingId) REFERENCES tb_MeetingUserMapping(Id) on delete cascade,
	Review varchar(500) NOT NULL,
  Rating FLOAT not NULL DEFAULT 0
);