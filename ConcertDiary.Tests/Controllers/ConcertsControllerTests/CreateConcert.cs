using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using ConcertDiary.Controllers;
using ConcertDiary.Exceptions;
using ConcertDiary.Models;
using ConcertDiary.Models.ResponseModels;
using ConcertDiary.Services;
using ConcertDiary.Tests.TestData.Concerts;
using deepequalitycomparer;
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
        private HttpRequestMessage _request;
        private Concert _concert;
        private readonly string _requestId = "8325e2f14e07411cadf9608dfaa98915";
        private readonly string _tweetUrl = "https://twitter.com/musiclover/status/987654321";

        [TestInitialize]
        public void Intialize()
        {
            _concertService = new Mock<IConcertService>();
            _tweetService = new Mock<ITweetService>();
            _controller = new ConcertsController(_concertService.Object, _tweetService.Object);

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
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void PostConcert_SendTweet_Successful()
        {
            var response = _controller.CreateConcert(true, _concert);
            Assert.AreEqual(_tweetUrl, response.TweetUrl);
        }

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void PostConcert_Returns_Concert()
        //{
        //    var concertResponse = ConcertTestData.GetCreatedConcert();
        //    var response = _controller.CreateConcert(true, _concert);
        //    Assert.IsTrue(DeepEqualityComparer.AreEqual(concertResponse, response.Concert));
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //public void PostConcert_DoNotSendTweet_Successful()
        //{
        //    var response = _controller.CreateConcert(false, _concert);
        //    Assert.AreEqual(string.Empty, response.TweetUrl);
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //[ExpectedException(typeof(ClientRequestException))]
        //public void PostConcert_ConcertIsNull_ThrowsException()
        //{
        //    _concert = null;

        //    try
        //    {
        //        _controller.CreateConcert(false, _concert);
        //    }
        //    catch (ClientRequestException ex)
        //    {
        //        Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        //        Assert.AreEqual("Failed to parse Concert.", ex.Message);
        //        throw;
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //[ExpectedException(typeof(ClientRequestException))]
        //public void PostConcert_ArtistIsNull_ThrowsException()
        //{
        //    _concert.Artist = null;

        //    try
        //    {
        //        _controller.CreateConcert(false, _concert);
        //    }
        //    catch (ClientRequestException ex)
        //    {
        //        Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        //        Assert.AreEqual("One or more Concert property is null or incorrect.", ex.Message);
        //        _concertService.Verify(s => s.CreateConcert(It.IsAny<string>(), It.IsAny<Concert>()), Times.Never);
        //        throw;
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //[ExpectedException(typeof(ClientRequestException))]
        //public void PostConcert_ArtistNameIsMissing_ThrowsException()
        //{
        //    _concert.Artist = new Artist {Name = string.Empty};

        //    try
        //    {
        //        _controller.CreateConcert(false, _concert);
        //    }
        //    catch (ClientRequestException ex)
        //    {
        //        Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        //        Assert.AreEqual("One or more Concert property is null or incorrect.", ex.Message);
        //        _concertService.Verify(s => s.CreateConcert(It.IsAny<string>(), It.IsAny<Concert>()), Times.Never);
        //        throw;
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //[ExpectedException(typeof(ClientRequestException))]
        //public void PostConcert_AppIdHeaderMissing_ThrowsException()
        //{
        //    _request.Headers.Remove("app_id");

        //    try
        //    {
        //        _controller.CreateConcert(false, _concert);
        //    }
        //    catch (ClientRequestException ex)
        //    {
        //        Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        //        Assert.AreEqual("Missing AppId header.", ex.Message);
        //        _concertService.Verify(s => s.CreateConcert(It.IsAny<string>(), It.IsAny<Concert>()), Times.Never);
        //        throw;
        //    }
        //}

        //[TestMethod]
        //[TestCategory("Unit")]
        //[ExpectedException(typeof(ClientRequestException))]
        //public void PostConcert_UserHeaderMissing_ThrowsException()
        //{
        //    _request.Headers.Remove("user");

        //    try
        //    {
        //        _controller.CreateConcert(false, _concert);
        //    }
        //    catch (ClientRequestException ex)
        //    {
        //        Assert.AreEqual(HttpStatusCode.BadRequest, ex.StatusCode);
        //        Assert.AreEqual("Missing UserId header.", ex.Message);
        //        _concertService.Verify(
        //            s => s.CreateConcert(It.IsAny<string>(), It.IsAny<Concert>()), Times.Never);
        //        throw;
        //    }
        //}
    }
}
