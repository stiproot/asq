use ASQ;

create table tb_BlogPost(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
	UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
	FOREIGN KEY (CreationUserId) REFERENCES tb_User(Id),
  Inactive bit not null,
	Title varchar(50) NOT NULL,
	Content text(65535) NOT NULL,
	ImgId bigint not null,
	FOREIGN key (ImgId) REFERENCES tb_Img(Id)
);

CREATE TABLE tb_FocusBlogPostMapping(
	Id BIGINT AUTO_INCREMENT NOT NULL PRIMARY KEY,
  UniqueId char(38) NOT NULL,
	CreationDateUtc datetime not null,
	CreationUserId bigint not null,
  Inactive bit not null,
	BlogPostId BIGINT not null,
	FOREIGN key (BlogPostId) REFERENCES tb_BlogPost(Id) on delete cascade,
	FocusId SMALLINT not null,
	FOREIGN key (FocusId) REFERENCES tb_Focus(Id),
	CONSTRAINT UC_BLOGPOST_FOCUS_MAPPING UNIQUE (BlogPostId, FocusId)
);