use ASQ;


select * from tb_Img


--update tb_Img
--set 
    --Path = 'http://localhost:3000/image/317bca08-51b8-41fa-a0e2-b646655b5923/heic1608a.jpg', 
    --ThumbnailPath = 'http://localhost:3000/image/317bca08-51b8-41fa-a0e2-b646655b5923/heic1608a.jpg'
--where Path = '317bca08-51b8-41fa-a0e2-b646655b5923/heic1608a.jpg'

update tb_Img
set 
    Path = 'http://localhost:3000/image/317bca08-51b8-41fa-a0e2-b646655b5923/heic1608a.jpg' ,
    ThumbnailPath = 'http://localhost:3000/image/317bca08-51b8-41fa-a0e2-b646655b5923/heic1608a.jpg' 
where Path = '317bca08-51b8-41fa-a0e2-b646655b5923/images (1).jpeg'
