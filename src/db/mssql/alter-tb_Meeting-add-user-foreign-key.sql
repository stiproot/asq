
use ASQ;

--[PK__tb_Meeti__3214EC07BD5125D7]
--[UQ__tb_Meeti__2CB664DC47E34F84]
--[FK__tb_Meetin__ExtMe__07220AB2]
--[FK__tb_Meetin__HostI__04459E07]
--[FK__tb_Meetin__ImgId__062DE679]
--[FK__tb_Meetin__Meeti__0539C240]
--[FK__tb_Meetin__Timez__3B95D2F1]

--alter table tb_Meeting add foreign key (CreationUserId) references tb_User(Id)

--select * from tb_User where Username = 'stiphost'

--select CreationUserId, * from tb_Meeting

--update tb_Meeting set CreationUserId = 15