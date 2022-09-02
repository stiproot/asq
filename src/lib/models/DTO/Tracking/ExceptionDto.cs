using System;
using System.Linq;
using System.Collections.Generic;

namespace DTO.Tracking
{
    public static class ExceptionModelFactory
    {
        public static ExceptionDto CreateExceptionDto(Exception ex)
        {
            return new ExceptionDto
            {
                ClassName = ex.GetType()?.Name,
                InnerException = ex.InnerException != null ? CreateExceptionDto(ex.InnerException) : null,
                Message = ex.Message,
                StackTrace = ex.StackTrace?.Split(Environment.NewLine).ToList()
            };
        }
    }

    public class ExceptionDto
    {
        public string ClassName{ get; set; }
        public string Message{ get; set; }
        public ExceptionDto InnerException{ get; set; }
        public IList<string> StackTrace{ get; set; }
    }
}