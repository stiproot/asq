use ASQ;

--select *
--from tb_Img
--order by Id DESC

--943fadc1-41da-4311-9f44-5c2f6ec941d1/694ce8a5-185c-44bf-9a07-4885c5c6cbe6.png
--943fadc1-41da-4311-9f44-5c2f6ec941d1/694ce8a5-185c-44bf-9a07-4885c5c6cbe6.png

--select *
--from tb_Meeting
--where Title like '%Title X%'

--insert into tb_MeetingRecording (UniqueId, CreationDateUtc, CreationUserId, MeetingId, [Path], Part, [FileName])
--values (newid(), getdate(), 0, 20006, '', 0, cast(newid() as nvarchar(36)) + '.mp4')

select * from tb_MeetingRecording

select * from tb_Meeting where Id = 20006
--select * from tb_Host where Id = 16
--select * from tb_User where HostId = 16
select * from tb_FocusMeetingMapping where MeetingId = 20006

--update tb_MeetingRecording
--set [Path] = 'http://localhost:3000/video/4ae91f37-68f1-41a2-bc56-a3e23f7ecbcc/75482d74-79b3-4d9d-9b9c-1574a32e674e.mp4', 
    --[FileName] = '75482d74-79b3-4d9d-9b9c-1574a32e674e.mp4'


