use ASQ;

-- select * from sys.foreign_keys

-- alter table tb_Meeting drop CONSTRAINT FK__tb_Meetin__Meeti__44952D46

-- drop table tb_MeetingStatus

-- ALTER TABLE tb_Meeting ADD FOREIGN KEY (MeetingStatusId) REFERENCES tb_MeetingStatus(Id);

-- alter table tb_PaymentInfo alter column ExpirationDate varchar(5)

alter table tb_Img alter column [Path] varchar(500) null;
alter table tb_Img alter column [ThumbnailPath] varchar(500) null;