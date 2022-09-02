using System;
using System.Collections.Generic;

namespace DTO.Zoom.User
{
    public class GetZoomUser 
    {
        public DateTime created_at{ get; set; }
        public IEnumerable<Attribute> custom_attributes{ get; set; }
        public string id { get; set; }
        public string first_name{ get; set; }
        public string last_name{ get; set; }
        public string email{ get; set; }
        public UserTypeEnu type{ get; set; }
        public string role_name{ get; set; }
        public long pmi{ get; set; }
        public bool use_pmi { get; set; }
        public string personal_meeting_url { get; set; }
        public string timezone { get; set; }
        public int verified { get; set; }
        public string dept { get; set; }
        public DateTime last_login_time { get; set; }
        public string last_clilent_version { get; set; }
        public string pic_url { get; set; }
        public string host_key { get; set; }
        public string jid { get; set; }
        public IEnumerable<int> group_ids { get; set; }
        public IEnumerable<string> im_group_ids { get; set; }
        public string account_id { get; set; }
        public string language { get; set; }
        public string phone_country { get; set; }
        public string phone_number { get; set; }
        public string status { get; set; }
    }
}