using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO.Zoom.Meeting
{
    public static class ExtMeetingCreationRequestFactory
    {
        public static CreateMeetingRequest CreateExtMeetingCreationRequestWithDefaults()
        {
            return new CreateMeetingRequest
            {
                topic = null,
                type = MeetingTypeEnu.Scheduled,
                start_time = DateTime.UtcNow,
                duration = 0,
                timezone = "Africa/Harare",
                password = null,
                agenda = null,
                settings = new MeetingSettings
                {
                    host_video = true,
                    participant_video = true,
                    cn_meeting = false,
                    in_meeting = false,
                    join_before_host = true,
                    mute_upon_entry = true,
                    watermark = false,
                    use_pmi = false,
                    approval_type = MeetingApprovalType.NoRegistrationRequired,
                    audio = "both",
                    auto_recording = "none",
                    alternate_hosts = null,
                    close_registration = true,
                    waiting_room = true,
                    global_dial_in_countries = null,
                    contact_name = null,
                    contact_email = null,
                    registrants_email_notification = false,
                    meeting_authentication = false,
                    authentication_option = null,
                    authentication_domains = null,
                    additional_data_center_regions = null
                }
            };
        }

        public static UpdateMeetingRequest CreateExtMeetingUpdateRequestFromOriginalResponse(CreateMeetingResponse original)
        {
            return new List<CreateMeetingResponse>()
            {
                original
            }
            .Select(o => new UpdateMeetingRequest
            {
                //schedule_for = 
                topic = o.topic,
                type = o.type,
                start_time = o.start_time,
                duration = o.duration,
                timezone = o.timezone,
                password = o.password,
                agenda = o.agenda,
                tracking_fields = o.tracking_fields,
                recurrence = o.recurrence,
                settings = o.settings
            })
            .First();
        }
    }
}