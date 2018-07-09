using System;
using System.Net;
using System.Web.Http;
using ConcertDiary.Exceptions;
using ConcertDiary.Models;
using ConcertDiary.Models.ResponseModels;
using ConcertDiary.Services;
using ConcertDiary.Validation;

namespace ConcertDiary.Controllers
{
    public class ConcertsController : ApiController
    { 
        private readonly IConcertService _concertService;
        private readonly ITweetService _tweetService;

        public ConcertsController(IConcertService concertService, ITweetService tweetService)
        {
            _concertService = concertService;
            _tweetService = tweetService;
        }

        [Route("api/concerts"), HttpGet]
        public ConcertsResponse GetConcerts()
        {
            var requestId = Guid.NewGuid().ToString("N");

            RequestValidator.ValidateWebRequestHeaders(Request);

            return _concertService.GetConcerts(requestId);
        }

        [Route("api/concerts/{id}"), HttpGet]
        public ConcertResponse GetConcert(long id)
        {
            var requestId = Guid.NewGuid().ToString("N");

            RequestValidator.ValidateWebRequestHeaders(Request);

            return _concertService.GetConcert(requestId, id);
        }

        [Route("api/concerts"), HttpPost]
        public CreateUpdateConcertResponse CreateConcert([FromUri] bool tweetConcert, [FromBody] Concert concert)
        {
            var requestId = Guid.NewGuid().ToString("N");

            RequestValidator.ValidateWebRequestHeaders(Request);

            if (concert == null)
                throw new ClientRequestException(HttpStatusCode.BadRequest, "Failed to parse Concert.");

            if (!concert.ValidateConcert())
                throw new ClientRequestException(HttpStatusCode.BadRequest, "One or more Concert property is null or incorrect.");

            var responseModel = _concertService.CreateConcert(requestId, concert);

            responseModel.TweetUrl = tweetConcert 
                ? _tweetService.TweetConcert(concert) 
                : string.Empty;

            return responseModel;
        }

        [Route("api/concerts/{id}"), HttpPut]
        public CreateUpdateConcertResponse UpdateConcert(long id, [FromUri] bool tweetConcert, [FromBody] Concert concert)
        {
            var requestId = Guid.NewGuid().ToString("N");

            RequestValidator.ValidateWebRequestHeaders(Request);
            ConcertValidator.ValidateUpdateConcert(concert);

            var responseModel = _concertService.UpdateConcert(requestId, concert);
            responseModel.TweetUrl = tweetConcert ? _tweetService.TweetConcert(concert) : string.Empty;

            return responseModel;
        }

        [Route("api/concerts/{id}"), HttpDelete]
        public RemoveConcertResponse RemoveConcert(long id)
        {
            var requestId = Guid.NewGuid().ToString("N");

            RequestValidator.ValidateWebRequestHeaders(Request);

            var responseModel = _concertService.RemoveConcert(requestId, id);

            return responseModel;
        }
    }
}
