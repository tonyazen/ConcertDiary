using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using ConcertDiary.Controllers;
using ConcertDiary.Ioc;
using ConcertDiary.Models.ResponseModels;
using ConcertDiary.Services;
using ConcertDiary.Tests.TestData.Concerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ConcertDiary.Tests.Controllers.BadConcerts2ControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetConcerts
    {
        private BadConcerts2Controller _controller;
        private Mock<ConcertService> _concertService;
        private Mock<TweetService> _tweetService;
        private Mock<LogWriter> _logger;
        private HttpRequestMessage _request;

        [TestInitialize]
        public void Intialize()
        {
            _concertService = new Mock<ConcertService>();
            _tweetService = new Mock<TweetService>();
            _logger = new Mock<LogWriter>();
            _controller = new BadConcerts2Controller(_logger.Object, _concertService.Object, _tweetService.Object);

            _request = new HttpRequestMessage();
            _request.Headers.Add("app_id", "testapp");
            _request.Headers.Add("user", "testuser");

            _controller.Request = _request;

            _concertService.Setup(s => s.GetConcerts(It.IsAny<string>()))
                .Returns(() => new ConcertsResponse
                {
                    Concerts = ConcertTestData.GetAllConcerts()
                });
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void GetConcerts_Successful()
        {
            var response = _controller.GetConcerts();
            Assert.AreEqual(response.Concerts.Length, 3);
        }
    }
}
