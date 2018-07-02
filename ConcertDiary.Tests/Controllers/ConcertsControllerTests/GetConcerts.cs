//using System.Diagnostics.CodeAnalysis;
//using System.Net.Http;
//using ConcertDiary.Controllers;
//using ConcertDiary.Models.ResponseModels;
//using ConcertDiary.Services;
//using ConcertDiary.Tests.TestData.Concerts;
//using log4net;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;

//namespace ConcertDiary.Tests.Controllers.ConcertsControllerTests
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class GetConcerts
//    {
//        private ConcertsController _controller;
//        private Mock<IConcertService> _concertService;
//        private Mock<ITweetService> _tweetService;
//        private HttpRequestMessage _request;

//        [TestInitialize]
//        public void Intialize()
//        {
//            _concertService = new Mock<IConcertService>();
//            _tweetService = new Mock<ITweetService>();
//            _controller = new ConcertsController(_concertService.Object, _tweetService.Object);

//            _request = new HttpRequestMessage();
//            _request.Headers.Add("app_id", "testapp");
//            _request.Headers.Add("user", "testuser");

//            _controller.Request = _request;

//            _concertService.Setup(s => s.GetConcerts(It.IsAny<string>()))
//                .Returns(() => new ConcertsResponse
//                {
//                    Concerts = ConcertTestData.GetAllConcerts()
//                });
//        }

//        [TestMethod]
//        [TestCategory("Unit")]
//        public void GetConcerts_Successful()
//        {
//            var response = _controller.GetConcerts();
//            Assert.AreEqual(response.Concerts.Length, 3);
//        }
//    }
//}
