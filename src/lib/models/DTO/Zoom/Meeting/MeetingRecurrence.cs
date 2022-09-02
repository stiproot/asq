using System;

namespace DTO.Zoom.Meeting
{
    public class MeetingRecurrence
    {
        public MeetingRecurrenceTypeEnu type{ get; set; }
        public int repeat_interval{ get; set; }
        public string weekly_days{ get; set; }
        public int monthly_day{ get; set; }
        public MonthlyWeekEnu monthly_week{ get; set; }
        public WeekDaysEnu monthly_week_day{ get; set; }
        public int end_times{ get; set; }
        // string
        public DateTime end_date_time{ get; set; }
    }
}