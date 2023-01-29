using System.Net;

namespace MigrationTask
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public CustomException(string message, Exception inner, HttpStatusCode statusCode) : base(message, inner)
        {
            StatusCode = statusCode;
        }
    }

}