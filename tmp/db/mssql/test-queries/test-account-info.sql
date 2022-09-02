use ASQ;

-- select top 10 * from tb_User order by Id DESC
select top 10 Tracking, * 
from tb_AccountCreationTracking 
where DAY(CreationDateUtc) = 28    
order by Id DESC

select * from tb_User 
where DAY(CreationDateUtc) = 28   
order by Id DESC

select * from tb_Host 
where DAY(CreationDateUtc) = 28    
order by Id DESC

select * from tb_ext_ZoomUser
where DAY(CreationDateUtc) = 28    
order by Id DESC

select * from tb_Img 
where DAY(CreationDateUtc) = 28    
order by Id DESC

select * from tb_PaymentInfo
where DAY(CreationDateUtc) = 28    
order by Id DESC

--[{"identifier":"persist-user","failed":true,"response":null,"exception_info":{"ClassName":"KeyNotFoundException","Message":"The given key \u0027user\u0027 was not present in the dictionary.","InnerException":null,"StackTrace":["   at System.Collections.Generic.Dictionary\u00602.get_Item(TKey key)","   at processes.Engine.PersistUserStrategyFactory.\u003C\u003Ec__DisplayClass8_0.\u003C\u003CCreateFactory\u003Eb__0\u003Ed.MoveNext() in /home/stiproot/Code/projects/asq/src/lib/processes/Engine/TaskWrapper.cs:line 364","--- End of stack trace from previous location where exception was thrown ---","   at processes.Engine.TaskWrapper.Run() in /home/stiproot/Code/projects/asq/src/lib/processes/Engine/TaskWrapper.cs:line 88"]}}]
