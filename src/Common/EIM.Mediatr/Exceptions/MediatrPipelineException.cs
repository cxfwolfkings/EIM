using System;

namespace EIM.Mediatr.Exceptions
{
    public class MediatrPipelineException : Exception
    {
        public MediatrPipelineException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
