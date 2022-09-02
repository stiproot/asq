CREATE TABLE tb_Vid(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId CHAR(38) NOT NULL,
	CreationDateUtc DATETIME NOT NULL,
	CreationUserId BIGINT NOT NULL, 
	Inactive BIT NOT NULL,
	FilePath VARCHAR(500) NOT NULL,
  Url VARCHAR(500) NOT NULL
);

CREATE TABLE tb_Video(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId CHAR(38) NOT NULL,
	CreationDateUtc DATETIME NOT NULL,
	CreationUserId BIGINT NOT NULL, 
	FOREIGN KEY (CreationUserId) REFERENCES tb_User(Id),
	Inactive BIT NOT NULL,
	Title VARCHAR(50) NOT NULL UNIQUE,
	Description VARCHAR (500) NOT NULL,
  Part SMALLINT NULL,
	VidId BIGINT NOT NULL,
	FOREIGN KEY (VidId) REFERENCES tb_Vid(Id),
	ImgId BIGINT NOT NULL,
	FOREIGN KEY (ImgId) REFERENCES tb_Img(Id),
	VideoGroupId CHAR(38) NULL
);

CREATE TABLE tb_FocusVideoMapping(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	VideoId BIGINT not null,
	FOREIGN key (VideoId) REFERENCES tb_Video(Id) on delete cascade,
	FocusId SMALLINT not null,
	FOREIGN key (FocusId) REFERENCES tb_Focus(Id),
	CONSTRAINT UC_VIDEO_FOCUS_MAPPING UNIQUE (VideoId, FocusId)
);
