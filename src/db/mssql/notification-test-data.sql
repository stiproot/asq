use ASQ;

SET IDENTITY_INSERT [tb_Notification] ON;

insert into tb_Notification(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [UserId], [Seen], [Title], [Message], [ImgUrl], [VideoUrl], [MeetingUrl], [ExtMeetingUrl], [NotificationTypeId])
values 
(
    1, 
    NEWID(), 
    GETDATE(), 
    0, 
    0, 
    2, 
    0, 
    'Meeting Successfully Created', 
    'Why I love plumbers', 
    'http://localhost:3000/image/2e5a8ade-bac6-4ccf-b9f5-538da2f84fa4/ec2b1da8-6e5b-44ee-9ad6-4951fa547a7d.jpeg',
    '',
    'htttp://localhost:4200/meeting/3837f0f9-30cc-4266-bd56-568f9dcf0fad',
    'https://zoom.us/s/94543626416?zak=eyJ6bV9za20iOiJ6bV9vMm0iLCJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJjbGllbnQiLCJ1aWQiOiJTMHN6dHN4TFJYR2diTjhmTTNyYU1nIiwiaXNzIjoid2ViIiwic3R5Ijo5OSwid2NkIjoiYXcxIiwiY2x0IjowLCJzdGsiOiI3QXN6UkZRb0hKNGtjSG9fQjJRZm5RemhVcnVEVUEtdU9kLUdZZ0dPa0o4LkJnWWdPRGM0V1ZVNFp6Uk1TRm94WkcweGJrVnlTMWRxZWpJNE9Xd3ZiMkZyV2pBQUFBd3pRMEpCZFc5cFdWTXpjejBBQTJGM01RQUFBWGhhcVA5RkFCSjFBQUFBIiwiZXhwIjoxNjI0MjA0NzI4LCJpYXQiOjE2MTY0Mjg3MjgsImFpZCI6IkI4RlN5WGVUUmJhSkhjbEVSQldCZHciLCJjaWQiOiIifQ.ZRE8IRSFZvq3EN6IYQGuVBhtzFid7PrQzHguxC_HwEo',
    0
)

SET IDENTITY_INSERT [tb_Notification] ON;

-- insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
-- values (1, NEWID(), GETDATE(), 0, 0, 'MEETING_PARTICIPATION')

-- insert into tb_NotificationType(Id, UniqueId, CreationDateUtc, CreationUserId, Inactive, [Description])
-- values (2, NEWID(), GETDATE(), 0, 0, 'MEETING_REMINDER')