using System;
using System.Collections.Generic;

namespace DTO.Zoom.Meeting
{
    public class MeetingSettings
    {
        public bool host_video{ get; set; }
        public bool participant_video{ get; set; }
        public bool cn_meeting{ get; set; }
        public bool in_meeting{ get; set; }
        public bool join_before_host{ get; set; }
        public bool mute_upon_entry{ get; set; }
        public bool watermark{ get; set; }
        public bool use_pmi{ get; set; }
        public MeetingApprovalType approval_type{ get; set; }
        public MeetingRegistrationType registration_type{ get; set; }
        //both/telephony/voip
        public string audio{ get; set; }
        //local/cloud/none
        public string auto_recording{ get; set; }
        // deprecated 
        //public bool enforce_login{ get; set; }
        // deprecated
        //public string enforce_login_domains{ get; set; }
        public string alternate_hosts{ get; set; }
        public bool close_registration{ get; set; }
        public bool waiting_room{ get; set; }
        public IEnumerable<string> global_dial_in_countries{ get; set; }
        public IEnumerable<GlobalDialInNumber> global_dial_in_numbers{ get; set; }
        public string contact_name{ get; set; }
        public string contact_email{ get; set; }
        public bool registrants_confirmation_email{ get; set; }
        public bool registrants_email_notification{ get; set; }
        public bool meeting_authentication{ get; set; }
        public string authentication_option{ get; set; }
        public string authentication_domains{ get; set; }
        public IEnumerable<string> additional_data_center_regions{ get; set; }
        public string athentication_name{ get; set; }
    }
}