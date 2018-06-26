using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using ConcertDiary.Controllers;
using ConcertDiary.Models;
using ConcertDiary.Tests.TestData.Concerts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcertDiary.Tests.Controllers.BadConcertsControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateConcert
    {
        private BadConcertsController _controller;
        private HttpRequestMessage _request;
        private Concert _concert;
        private const string _requestId = "8325e2f14e07411cadf9608dfaa98915";
        private const string _tweetUrl = "https://twitter.com/musiclover/status/987654321";

        [TestInitialize]
        public void Intialize()
        {
            _controller = new BadConcertsController();

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
