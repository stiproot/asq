namespace DTO.Tracking
{
    public class TrackingComponent
    {
        public string identifier{ get; set; }
        public bool? failed{ get; set; }
        public object response{ get; set; }
        public ExceptionDto exception_info{ get; set; } 
    }
}