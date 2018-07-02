using System;
using System.Net;

namespace ConcertDiary.Exceptions
{
    public class ClientRequestException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ClientRequestException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}