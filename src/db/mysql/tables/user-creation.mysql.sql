use ASQ;

create table tb_AccountCreationTracking(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Request JSON NOT NULL,
	Tracking JSON NOT NULL,
  Failed bit not null
);

create table tb_AccountUpdateTracking(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Request JSON NOT NULL,
	Tracking JSON NOT NULL,
  Failed bit not null
);

CREATE TABLE tb_Img(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null, 
	Inactive bit not null,
	Url varchar(500) not null,
	ThumbnailUrl varchar(500) not null
);

CREATE TABLE tb_AccountType(
	Id SMALLINT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Description varchar(25) NOT NULL
);

CREATE TABLE tb_CardType(
	Id int AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Description varchar(25) NOT NULL
);

CREATE TABLE tb_Focus(
	Id SMALLINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Description varchar(25) NOT NULL
);

CREATE TABLE tb_ext_ZoomUser(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Payload JSON NOT NULL
);

CREATE TABLE tb_Host(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
  IsConsultant bit null default 0,
	Company varchar(50) NOT NULL,
	CareerSummary varchar(1500) NOT NULL,
	ExtUserId BIGINT not null,
	FOREIGN KEY (ExtUserId) REFERENCES tb_ext_ZoomUser(Id)
);

CREATE TABLE tb_PaymentInfo(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	CardNumber varchar(20) NOT NULL UNIQUE,
	ExpirationDate date NOT NULL,
	Cvc varchar(5) not null,
	CardTypeId INT not null,
	FOREIGN KEY (CardTypeId) REFERENCES tb_CardType(Id)
);

CREATE TABLE tb_Timezone(
	Id smallint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
    Inactive bit not null,
	Display varchar(50) NOT NULL UNIQUE,
	UtcOffset tinyint NOT NULL,
	ExtCode varchar(50) NOT NULL UNIQUE,
	CONSTRAINT UC_DISPLAY_EXCODE UNIQUE (Display, ExtCode)
);

CREATE TABLE tb_User(
	Id bigint AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	Username varchar(25) NOT NULL UNIQUE,
	Name varchar(25) NOT NULL,
	Surname varchar(25) NOT NULL,
	Email varchar(50) NOT NULL UNIQUE,
	Password varchar(25) NOT NULL,
	AccountTypeId SMALLINT not null,
	ImgId bigint not null,
	HostId bigint null,
  TimezoneId smallint not null,
	PaymentInfoId BIGINT null,
	FOREIGN KEY (AccountTypeId) REFERENCES tb_AccountType(Id),
	FOREIGN KEY (ImgId) REFERENCES tb_Img(Id),
	FOREIGN KEY (HostId) REFERENCES tb_Host(Id),
	FOREIGN key (TimezoneId) REFERENCES tb_Timezone(Id),
	FOREIGN key (PaymentInfoId) REFERENCES tb_PaymentInfo(Id)
);

CREATE TABLE tb_FocusUserMapping(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	UserId BIGINT not null,
	FocusId SMALLINT not null,
	FOREIGN key (UserId) REFERENCES tb_User(Id) on delete cascade,
	FOREIGN key (FocusId) REFERENCES tb_Focus(Id),
	CONSTRAINT UC_USER_FOCUS_MAPPING UNIQUE (UserId, FocusId)
);

CREATE TABLE tb_FocusHostMapping(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	HostId BIGINT not null,
	FocusId SMALLINT not null,
	FOREIGN key (HostId) REFERENCES tb_Host(Id) on delete cascade,
	FOREIGN key (FocusId) REFERENCES tb_Focus(Id),
	CONSTRAINT UC_HOST_FOCUS_MAPPING UNIQUE (HostId, FocusId)
);