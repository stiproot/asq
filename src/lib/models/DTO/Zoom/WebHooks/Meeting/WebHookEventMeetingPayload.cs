namespace DTO.Zoom.WebHook.Meeting
{
    public class WebHookEventMeetingPayload
    {
        // meeting.participant_joined
        // meeting.participant_left
        // meeting.participant_admitted
        // meeting.participant_jbh_waiting
        // meeting.created 
        // meeting.started 
        // meeting.ended 
        // meeting.updated 
        // meeting.deleted 
        // meeting.sharing_started 
        // meeting.sharing_ended 
        public string account_id{ get; set; }
        // meeting.created
        // meeting.updated 
        // meeting.deleted 
        public string @operator{ get; set; }
        // ALL
        public Zoom.Meeting.Meeting @object{ get; set; }
        // meeting.created
        // meeting.updated 
        public string operator_id{ get; set; }
        // meeting.created
        // meeting.updated 
        public string operation{ get; set; }

        // meeting.updated
        // old_object
    }
}