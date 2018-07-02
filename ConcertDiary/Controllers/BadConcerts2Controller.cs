//using System;
//using System.Net;
//using System.Web.Http;
//using ConcertDiary.Exceptions;
//using ConcertDiary.Ioc;
//using ConcertDiary.Models;
//using ConcertDiary.Models.ResponseModels;
//using ConcertDiary.Services;
//using ConcertDiary.Validation;
//using log4net;

//namespace ConcertDiary.Controllers
//{
//    public class BadConcerts2Controller : ApiController
//    {
//        private readonly LogWriter _logger;
//        private readonly ConcertService _concertService;
//        private readonly TweetService _tweetService;

//        public BadConcerts2Controller(LogWriter logger, ConcertService concertService, TweetService tweetService)
//        {
//            _logger = logger;
//            _concertService = concertService;
//            _tweetService = tweetService;
//        }

//        [Route("api/concerts"), HttpGet]
//        public ConcertsResponse GetConcerts()
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            RequestValidator.ValidateWebRequest(Request);

//            return _concertService.GetConcerts(requestId);
//        }

//        [Route("api/concerts/{id}"), HttpGet]
//        public ConcertResponse GetConcert(int id)
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            RequestValidator.ValidateWebRequest(Request);

//            return _concertService.GetConcert(requestId, id);
//        }

//        [Route("api/concerts/{id}"), HttpPost]
//        public CreateUpdateConcertResponse CreateConcert(bool tweetConcert, [FromBody] Concert concert)
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            if (concert == null)
//            {
//                var errorMessage = $"Failed to parse request body for request ID {requestId}.";
//                _logger.ErrorFormat(errorMessage);
//                throw new ClientRequestException(HttpStatusCode.BadRequest, errorMessage);
//            }

//            RequestValidator.ValidateWebRequest(Request);

//            var responseModel = _concertService.CreateConcert(requestId, concert);
//            responseModel.TweetUrl = tweetConcert ? _tweetService.TweetConcert(concert).ToString() : string.Empty;

//            return responseModel;
//        }

//        [Route("api/concerts/{id}"), HttpPut]
//        public CreateUpdateConcertResponse UpdateConcert(bool tweetConcert, [FromBody] Concert concert)
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            if (concert == null)
//            {
//                var errorMessage = $"Failed to parse request body for request ID {requestId}.";
//                _logger.ErrorFormat(errorMessage);
//                throw new ClientRequestException(HttpStatusCode.BadRequest, errorMessage);
//            }

//            RequestValidator.ValidateWebRequest(Request);

//            var responseModel = _concertService.CreateConcert(requestId, concert);
//            responseModel.TweetUrl = tweetConcert ? _tweetService.TweetConcert(concert).ToString() : string.Empty;

//            return responseModel;

//        }

//        [Route("api/concerts/{id}"), HttpDelete]
//        public RemoveConcertResponse RemoveConcert(long id)
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            RequestValidator.ValidateWebRequest(Request);

//            var responseModel = _concertService.RemoveConcert(requestId, id);

//            return responseModel;
//        }
//    }
//}
