using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using ConcertDiary.Controllers;
using ConcertDiary.Ioc;
using ConcertDiary.Models;
using ConcertDiary.Services;
using ConcertDiary.Tests.TestData.Concerts;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConcertDiary.Tests.Controllers.ConcertsControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateConcertWithDI
    {
        private ConcertsController _controller;
        private ConcertService _concertService;
        private TweetService _tweetService;
        private LogWriter _logger;
        private HttpRequestMessage _request;
        private Concert _concert;
        private readonly string _requestId = "8325e2f14e07411cadf9608dfaa98915";
        private readonly string _tweetUrl = "https://twitter.com/musiclover/status/987654321";

        [TestInitialize]
        public void Intialize()
        {
            _concertService = new ConcertService();
            _tweetService = new TweetService();
            _logger = new LogWriter();
            _controller = new ConcertsController(_logger, _concertService, _tweetService);

            _request = new HttpRequestMessage();
            _request.Headers.Add("app_id", "testapp");
            _request.Headers.Add("user", "testuser");

            _controller.Request = _request;

            _concert = ConcertTestData.GetConcertToCreate();
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostConcert_Returns_Successful()
        {
            var response = _controller.CreateConcert(true, _concert);
            Assert.AreEqual(1237, response.Concert.Id);
            Assert.AreEqual(_requestId, response.RequestId);
            Assert.AreEqual(_tweetUrl, response.TweetUrl);
        }

    }
}
