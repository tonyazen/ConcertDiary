using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using ConcertDiary.Controllers;
using ConcertDiary.Ioc;
using ConcertDiary.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConcertDiary.Tests.Controllers.BadConcertsControllerTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetConcerts
    {
        private BadConcertsController _controller;
        private HttpRequestMessage _request;

        [TestInitialize]
        public void Intialize()
        {
            _controller = new BadConcertsController();

            _request = new HttpRequestMessage();
            _request.Headers.Add("app_id", "testapp");
            _request.Headers.Add("user", "testuser");

            _controller.Request = _request;
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
