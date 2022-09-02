namespace processes.Models
{
    public class ProcessResult
    {
        public bool Failed{ get; set; }
        public ExceptionInfo ExceptionInfo{ get; set; }
        public object Response{ get; set; }
    }
}