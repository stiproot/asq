use ASQ;

select * from tb_ext_ZoomMeeting

--{"id":94543626416,"topic":"ASQ 101","type":2,"start_time":"2021-03-31T15:00:02Z","duration":135,"timezone":"Africa/Johannesburg","created_at":"2021-03-22T15:58:48Z","agenda":"This application is revolutionary","start_url":"https://zoom.us/s/94543626416?zak=eyJ6bV9za20iOiJ6bV9vMm0iLCJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJjbGllbnQiLCJ1aWQiOiJTMHN6dHN4TFJYR2diTjhmTTNyYU1nIiwiaXNzIjoid2ViIiwic3R5Ijo5OSwid2NkIjoiYXcxIiwiY2x0IjowLCJzdGsiOiI3QXN6UkZRb0hKNGtjSG9fQjJRZm5RemhVcnVEVUEtdU9kLUdZZ0dPa0o4LkJnWWdPRGM0V1ZVNFp6Uk1TRm94WkcweGJrVnlTMWRxZWpJNE9Xd3ZiMkZyV2pBQUFBd3pRMEpCZFc5cFdWTXpjejBBQTJGM01RQUFBWGhhcVA5RkFCSjFBQUFBIiwiZXhwIjoxNjI0MjA0NzI4LCJpYXQiOjE2MTY0Mjg3MjgsImFpZCI6IkI4RlN5WGVUUmJhSkhjbEVSQldCZHciLCJjaWQiOiIifQ.ZRE8IRSFZvq3EN6IYQGuVBhtzFid7PrQzHguxC_HwEo","join_url":"https://zoom.us/j/94543626416","password":null,"h323_password":null,"pmi":0,"tracking_fields":null,"settings":{"host_video":true,"participant_video":true,"cn_meeting":false,"in_meeting":false,"join_before_host":true,"mute_upon_entry":true,"watermark":false,"use_pmi":false,"approval_type":2,"registration_type":0,"audio":"both","auto_recording":"none","alternate_hosts":null,"close_registration":false,"waiting_room":true,"global_dial_in_countries":["US"],"global_dial_in_numbers":[{"city":null,"country":"US","country_name":"US","number":"\u002B1 6699006833","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 9292056099","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 2532158782","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 3017158592","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 3126266799","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 3462487799","type":"toll"}],"contact_name":null,"contact_email":null,"registrants_confirmation_email":true,"registrants_email_notification":false,"meeting_authentication":false,"authentication_option":null,"authentication_domains":null,"additional_data_center_regions":null,"athentication_name":null},"recurrence":null}

 update tb_ext_ZoomMeeting set Payload = '{"id":93811978504,"topic":"ASQ 101","type":2,"start_time":"2021-03-31T15:00:02Z","duration":135,"timezone":"Africa/Johannesburg","created_at":"2021-03-22T15:58:48Z","agenda":"This application is revolutionary","start_url":"https://zoom.us/s/94543626416?zak=eyJ6bV9za20iOiJ6bV9vMm0iLCJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJjbGllbnQiLCJ1aWQiOiJTMHN6dHN4TFJYR2diTjhmTTNyYU1nIiwiaXNzIjoid2ViIiwic3R5Ijo5OSwid2NkIjoiYXcxIiwiY2x0IjowLCJzdGsiOiI3QXN6UkZRb0hKNGtjSG9fQjJRZm5RemhVcnVEVUEtdU9kLUdZZ0dPa0o4LkJnWWdPRGM0V1ZVNFp6Uk1TRm94WkcweGJrVnlTMWRxZWpJNE9Xd3ZiMkZyV2pBQUFBd3pRMEpCZFc5cFdWTXpjejBBQTJGM01RQUFBWGhhcVA5RkFCSjFBQUFBIiwiZXhwIjoxNjI0MjA0NzI4LCJpYXQiOjE2MTY0Mjg3MjgsImFpZCI6IkI4RlN5WGVUUmJhSkhjbEVSQldCZHciLCJjaWQiOiIifQ.ZRE8IRSFZvq3EN6IYQGuVBhtzFid7PrQzHguxC_HwEo","join_url":"https://zoom.us/j/94543626416","password":null,"h323_password":null,"pmi":0,"tracking_fields":null,"settings":{"host_video":true,"participant_video":true,"cn_meeting":false,"in_meeting":false,"join_before_host":true,"mute_upon_entry":true,"watermark":false,"use_pmi":false,"approval_type":2,"registration_type":0,"audio":"both","auto_recording":"none","alternate_hosts":null,"close_registration":false,"waiting_room":true,"global_dial_in_countries":["US"],"global_dial_in_numbers":[{"city":null,"country":"US","country_name":"US","number":"\u002B1 6699006833","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 9292056099","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 2532158782","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 3017158592","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 3126266799","type":"toll"},{"city":null,"country":"US","country_name":"US","number":"\u002B1 3462487799","type":"toll"}],"contact_name":null,"contact_email":null,"registrants_confirmation_email":true,"registrants_email_notification":false,"meeting_authentication":false,"authentication_option":null,"authentication_domains":null,"additional_data_center_regions":null,"athentication_name":null},"recurrence":null}'
 where Id = 16