using System;
using System.Diagnostics;

namespace processes.Models
{
    public class ExceptionInfo
    {
        public Exception Ex{ get; set; }
        public StackTrace Stack{ get; set; }
        public Exception InnerEx{ get; set; }
    }
}