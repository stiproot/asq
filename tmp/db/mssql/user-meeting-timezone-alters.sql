use ASQ;

-- alter table tb_Host ADD IsConsultant bit null 
-- update tb_Host set IsConsultant = 0
alter table tb_Host alter column IsConsultant bit not NULL


-- alter table tb_User ADD TimezoneId SMALLINT null foreign key REFERENCES tb_Timezone(Id)
-- alter table tb_Meeting ADD TimezoneId SMALLINT null foreign key REFERENCES tb_Timezone(Id)
-- GO

-- insert into tb_Timezone(UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], UtcOffset, ExtCode)
-- values (NEWID(), GETDATE(), 0, 0, 'Midway Island, Samoa', 13, 'Pacific/Midway')

-- insert into tb_Timezone(UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], UtcOffset, ExtCode)
-- values (NEWID(), GETDATE(), 0, 0, 'Hong Kong', 8, 'Asia/Hong_Kong')

-- insert into tb_Timezone(UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], UtcOffset, ExtCode)
-- values (NEWID(), GETDATE(), 0, 0, 'Harare, Pretoria', 2, 'Africa/Harare')

-- insert into tb_Timezone(UniqueId, CreationDateUtc, CreationUserId, Inactive, [Display], UtcOffset, ExtCode)
-- values (NEWID(), GETDATE(), 0, 0, 'London', 1, 'Europe/London')

--select * from tb_Timezone
-- update tb_User set TimezoneId = 1
-- update tb_Meeting set TimezoneId = 1

-- alter table tb_User alter column TimezoneId SMALLINT not NULL
-- alter table tb_Meeting alter column TimezoneId SMALLINT not NULL

-- alter table tb_Timezone add CONSTRAINT UC_DISPLAY_EXTCODE UNIQUE (Display, ExtCode)