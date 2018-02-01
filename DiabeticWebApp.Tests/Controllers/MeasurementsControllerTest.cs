using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using DiabeticWebApp.Controllers;
using DiabeticWebApp.Models;
using DiabeticWebApp.Repository.MeasurementRepositories;
using DiabeticWebApp.Service.MeasurementService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DiabeticWebApp.Tests.Controllers
{
    [TestClass]
    public class MeasurementsControllerTest
    {
        private const string UserId = "asd";
        private const string TestDateString = "5/1/2008 8:30:52 AM";
        private const string TestDescription = "test description 123!";
        private const int TestResult = 123;

        [TestMethod]
        public void GetReturnsMeasurement()
        {
            // Arrange

            var mockService = new Mock<IMeasurementsService>();
            mockService.Setup(x => x.GetMeasurement(UserId, 1))
                .Returns(new MeasurementDto
                {
                    Id = 1,
                    Date = DateTime.Parse(TestDateString),
                    Description = TestDescription,
                    Result = TestResult
                });

            var controller = new MeasurementsController(mockService.Object);

            // Act
            IHttpActionResult actionResult = controller.GetMeasurement(1);
            var contentResult = actionResult as OkNegotiatedContentResult<MeasurementDto>;


            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
            Assert.AreEqual(DateTime.Parse(TestDateString), contentResult.Content.Date);
            Assert.AreEqual(TestDescription, contentResult.Content.Description);
            Assert.AreEqual(TestResult, contentResult.Content.Result);
        }

    }
}
