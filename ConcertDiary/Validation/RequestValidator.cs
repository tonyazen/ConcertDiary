using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using ConcertDiary.Configuration;
using ConcertDiary.Exceptions;

namespace ConcertDiary.Validation
{
    public static class RequestValidator
    {
        public static void ValidateWebRequestHeaders(HttpRequestMessage request)
        {
            var appIdHeader = ConfigurationValues.AppIdHeader;
            var userIdHeader = ConfigurationValues.UserIdHeader;

            if (!request.Headers.Contains(appIdHeader) ||
                request.Headers.GetValues(appIdHeader).All(string.IsNullOrWhiteSpace))
            {
                throw new ClientRequestException(HttpStatusCode.BadRequest, $"Missing AppId header.");
            }

            if (!request.Headers.Contains(userIdHeader) ||
                request.Headers.GetValues(userIdHeader).All(string.IsNullOrWhiteSpace))
            {
                throw new ClientRequestException(HttpStatusCode.BadRequest, $"Missing UserId header.");
            }
        }
    }
}