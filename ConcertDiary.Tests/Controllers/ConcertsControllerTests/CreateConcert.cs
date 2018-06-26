using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using ConcertDiary.Controllers;
using ConcertDiary.Models;
using ConcertDiary.Models.ResponseModels;
using ConcertDiary.Services;
using ConcertDiary.Tests.TestData.Concerts;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConcertDiary.Tests.Controllers.ConcertsControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateConcert
    {
        private ConcertsController _controller;
        private Mock<IConcertService> _concertService;
        private Mock<ITweetService> _tweetService;
        private Mock<ILog> _logger;
        private HttpRequestMessage _request;
        private Concert _concert;
        private readonly string _requestId = "8325e2f14e07411cadf9608dfaa98915";
        private readonly string _tweetUrl = "https://twitter.com/musiclover/status/987654321";

        [TestInitialize]
        public void Intialize()
        {
            _concertService = new Mock<IConcertService>();
            _tweetService = new Mock<ITweetService>();
            _logger = new Mock<ILog>();
            _controller = new ConcertsController(_logger.Object, _concertService.Object, _tweetService.Object);

            _request = new HttpRequestMessage();
            _request.Headers.Add("app_id", "testapp");
            _request.Headers.Add("user", "testuser");

            _controller.Request = _request;

            _concertService.Setup(s => s.CreateConcert(It.IsAny<string>(), It.IsAny<Concert>()))
                .Returns(() => new CreateUpdateConcertResponse
                {
                    RequestId = _requestId,
                    Concert = ConcertTestData.GetCreatedConcert(),
                });

            _tweetService.Setup(s => s.TweetConcert(It.IsAny<Concert>()))
                .Returns(() => _tweetUrl);

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

        [TestMethod]
        [TestCategory("Unit")]
        public void PostConcert_SendTweet_Successful()
        {
            var response = _controller.CreateConcert(true, _concert);
            Assert.AreEqual(_tweetUrl, response.TweetUrl);
        }

    }
}
