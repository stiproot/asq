using System;
using System.Collections.Generic;

namespace DTO.Zoom.Meeting
{
    public class GetListMeetingResponse
    {
        public int page_count{ get; set; }
        public int page_number{ get; set; }
        public int page_size{ get; set; }
        public int total_records{ get; set; }
        public string next_page_token{ get; set; }
        IEnumerable<Meeting> meetings{ get; set; }
    }
}