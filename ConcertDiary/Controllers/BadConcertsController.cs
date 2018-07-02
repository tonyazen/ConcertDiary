//using System;
//using System.Net;
//using System.Runtime.InteropServices.WindowsRuntime;
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
//    public class BadConcertsController : ApiController
//    {
//        private readonly ILog _logger = new LogWriter();

//        [Route("api/concerts"), HttpGet]
//        public ConcertsResponse GetConcerts()
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            RequestValidator.ValidateWebRequest(Request);

//            var concertService = new ConcertService();

//            return concertService.GetConcerts(requestId);
//        }

//        [Route("api/concerts/{id}"), HttpGet]
//        public ConcertResponse GetConcert(int id)
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            RequestValidator.ValidateWebRequest(Request);

//            var concertService = new ConcertService();

//            return concertService.GetConcert(requestId, id);
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

//            var concertService = new ConcertService();
//            var tweetService = new TweetService();

//            var responseModel = concertService.CreateConcert(requestId, concert);

//            responseModel.TweetUrl = tweetConcert ? tweetService.TweetConcert(concert).ToString() : string.Empty;

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

//            var concertService = new ConcertService();
//            var tweetService = new TweetService();

//            var responseModel = concertService.CreateConcert(requestId, concert);
//            responseModel.TweetUrl = tweetConcert ? tweetService.TweetConcert(concert).ToString() : string.Empty;

//            return responseModel;
//        }

//        [Route("api/concerts/{id}"), HttpDelete]
//        public RemoveConcertResponse RemoveConcert(long id)
//        {
//            var requestId = Guid.NewGuid().ToString("N");

//            RequestValidator.ValidateWebRequest(Request);

//            var concertService = new ConcertService();

//            var responseModel = concertService.RemoveConcert(requestId, id);

//            return responseModel;
//        }

//    }
//}