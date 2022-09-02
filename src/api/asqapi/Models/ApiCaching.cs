namespace asqapi.Models
{
  public sealed class ApiCaching
  {
    public int MeetingQueryExpirationMinutes{ get; set; }
    public int MeetingSummaryExpirationMinute{ get; set; }
  }
}