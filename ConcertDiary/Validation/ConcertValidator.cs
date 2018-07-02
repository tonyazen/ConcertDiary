using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using ConcertDiary.Exceptions;
using ConcertDiary.Models;

namespace ConcertDiary.Validation
{
    public class ConcertValidator
    {
        public static void ValidateCreateConcert(Concert concert)
        {
            if (concert == null)
                throw new ClientRequestException(HttpStatusCode.BadRequest, "Failed to parse Concert.");

            if (!concert.ValidateConcert())
                throw new ClientRequestException(HttpStatusCode.BadRequest, "Concert or one of its properties is null or incorrect.");
        }

        public static void ValidateUpdateConcert(Concert concert)
        {
            if (concert == null)
                throw new ClientRequestException(HttpStatusCode.BadRequest, "Failed to parse Concert.");

            if (concert.Id == 0)
                throw new ClientRequestException(HttpStatusCode.BadRequest, "Concert ID is required.");

            if (!concert.ValidateConcert())
                throw new ClientRequestException(HttpStatusCode.BadRequest, "Concert or one of its properties is null or incorrect.");
        }
    }
}