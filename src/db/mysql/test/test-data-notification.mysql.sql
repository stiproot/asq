use ASQ;

insert into tb_Notification(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, UserId, Seen, Title, Message, ImgUrl, VideoUrl, MeetingUrl, ExtMeetingUrl, NotificationTypeId)
values 
(
    1, 
    UUID(), 
    CURDATE(), 
    0, 
    0, 
    2, 
    0, 
    'Meeting Successfully Created', 
    'Why I love plumbers', 
    'http://localhost:3000/image/2e5a8ade-bac6-4ccf-b9f5-538da2f84fa4/ec2b1da8-6e5b-44ee-9ad6-4951fa547a7d.jpeg',
    '',
    'htttp://localhost:4200/meeting/3837f0f9-30cc-4266-bd56-568f9dcf0fad',
    'https://zoom.us/s/94543626?zak=eyJ6bV9za20iOiJ6bV9vMm0iL.uijkkjjkjkkj.ZRE8IRSFZvq3EN6IYQGuVBhtzFid7PrQzHguxC_HwEo',
    0
)