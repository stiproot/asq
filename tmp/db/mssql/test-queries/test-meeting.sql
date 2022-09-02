use ASQ;

select top 10 Tracking, * 
from tb_MeetingCreationTracking 
where DAY(CreationDateUtc) = 29    
order by Id DESC

select * from tb_ext_ZoomMeeting
where DAY(CreationDateUtc) = 29    
order by Id DESC

select * from tb_Meeting 
where DAY(CreationDateUtc) = 29    
order by Id DESC