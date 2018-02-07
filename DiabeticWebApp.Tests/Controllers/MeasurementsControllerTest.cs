using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
using Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DiabeticWebApp.Tests.Controllers
{
    [TestClass]
    public class MeasurementsControllerTest
    {
        private Mock<IMeasurementsService> _mockService;
        private MeasurementsController _controller;

        private const string TestDateString = "5/1/2008 8:30:52 AM";
        private const string TestDescription = "test description 123!";
        private const int TestResult = 123;

        private readonly MeasurementDto _testValidMeasurement = new MeasurementDto
        {
            Id = 1,
            Date = DateTime.Parse(TestDateString),
            Description = TestDescription,
            Result = TestResult
        };

        private readonly MeasurementDto _testInvalidMeasurementWithoutDate = new MeasurementDto
        {
            Id = 1,
            Description = TestDescription,
            Result = TestResult
        };

        private readonly MeasurementDto _testInvalidMeasurementWithoutResult = new MeasurementDto
        {
            Id = 1,
            Date = DateTime.Parse(TestDateString),
            Description = TestDescription,
        };

        private readonly MeasurementDto _testInvalidMeasurementWithNegativeResult = new MeasurementDto
        {
            Id = 1,
            Date = DateTime.Parse(TestDateString),
            Description = TestDescription,
            Result = -25
        };

        private readonly List<MeasurementDto> _measurementsList = new List<MeasurementDto>{
            new MeasurementDto {
                Id = 1,
                Date = DateTime.Parse("24/2/2018 9:54:22 AM"),
                Description = "Description one",
                Result = 11
            },
            new MeasurementDto {
                Id = 2,
                Date = DateTime.Parse("23/3/2018 3:23:11 PM"),
                Description = "Description two",
                Result = 22
            },
            new MeasurementDto {
                Id = 3,
                Date = DateTime.Parse("24/3/2018 9:54:22 AM"),
                Description = "Description three",
                Result = 11
            },
            new MeasurementDto {
                Id = 4,
                Date = DateTime.Parse("4/4/2018 3:23:11 PM"),
                Description = "Description four",
                Result = 22
            },
            new MeasurementDto {
                Id = 5,
                Date = DateTime.Parse("15/4/2018 9:54:22 AM"),
                Description = "Description five",
                Result = 11
            },
            new MeasurementDto {
                Id = 6,
                Date = DateTime.Parse("12/5/2018 3:23:11 PM"),
                Description = "Description six",
                Result = 22
            }

        };

        [TestInitialize]
        public void SetUp()
        {
            _mockService = new Mock<IMeasurementsService>();
            _controller = new MeasurementsController(_mockService.Object);
        }

        [TestMethod]
        public void GetMeasurement_ShouldReturnOkAndMeasurement()
        {
            _mockService.Setup(x => x.GetMeasurement(It.IsAny<string>(), 1))
                .Returns(_testValidMeasurement);

            var response = _controller.GetMeasurement(1);
            var contentResult = response as OkNegotiatedContentResult<MeasurementDto>;         

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
            Assert.AreEqual(DateTime.Parse(TestDateString), contentResult.Content.Date);
            Assert.AreEqual(TestDescription, contentResult.Content.Description);
            Assert.AreEqual(TestResult, contentResult.Content.Result);
        }

        [TestMethod]
        public void GetAllMeasurements_ReturnsOkAndMeasurementsList()
        {
            _mockService.Setup(x => x.GetMeasurements(It.IsAny<string>()))
                .Returns(_measurementsList);

            var response = _controller.GetMeasurements();
            var contentResult = response as OkNegotiatedContentResult<List<MeasurementDto>>;

            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(_measurementsList, contentResult.Content);
        }

        [TestMethod]
        public void GetMeasurement_ShouldReturnsNotFound()
        {
            _mockService.Setup(x => x.GetMeasurement(It.IsAny<string>(), 1));

            var response = _controller.GetMeasurement(1);
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
        }

        [TestMethod]
        public void PostValidMeasurement_ShouldReturnsOk()
        {
            _mockService.Setup(x => x.AddMeasurement(It.IsAny<string>(), _testValidMeasurement));

            var response = _controller.PostMeasurement(_testValidMeasurement);

            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void PostInvalidMeasurementWhenDateIsNotProvided_ShouldReturnsInvalidModelState()
        {
            var validationContext = new ValidationContext(_testInvalidMeasurementWithoutDate, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_testInvalidMeasurementWithoutDate, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            var response = _controller.PostMeasurement(_testInvalidMeasurementWithoutDate);

            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void PostInvalidMeasurementWhenResultIsNotProvided_ShouldReturnsInvalidModelState()
        {
            var validationContext = new ValidationContext(_testInvalidMeasurementWithoutResult, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_testInvalidMeasurementWithoutResult, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            var response = _controller.PostMeasurement(_testInvalidMeasurementWithoutResult);

            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void PostInvalidMeasurementWhenResultIsNegativeNumber_ShouldReturnsInvalidModelState()
        {
            var validationContext = new ValidationContext(_testInvalidMeasurementWithNegativeResult, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(_testInvalidMeasurementWithNegativeResult, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            var response = _controller.PostMeasurement(_testInvalidMeasurementWithNegativeResult);

            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void PutMeasurementUsingValidData_ShouldReturnsOk()
        {
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);
            _mockService.Setup(x => x.UpdateMeasurement(It.IsAny<string>(), _testValidMeasurement));          
            _controller.PostMeasurement(_testValidMeasurement);

            var response = _controller.PutMeasurement(_testValidMeasurement.Id, _measurementsList[2]);

            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void PutNotExistingMeasurement_ShouldReturnsNotFound()
        {
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);
            _mockService.Setup(x => x.UpdateMeasurement(It.IsAny<string>(), _testValidMeasurement));

            var response = _controller.PutMeasurement(_testValidMeasurement.Id, _measurementsList[2]);
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
        }

        [TestMethod]
        public void DeleteExisitngMeasurement_ShouldReturnsOk()
        {
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);
            _mockService.Setup(x => x.DeleteMeasurement(It.IsAny<string>(), It.IsAny<int>()));

            var response = _controller.DeleteMeasurement(1);

            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteNotExisitngMeasurement_ShouldReturnsNotFound()
        {
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);
            _mockService.Setup(x => x.DeleteMeasurement(It.IsAny<string>(), It.IsAny<int>()));

            var response = _controller.DeleteMeasurement(1);
            var contentResult = response as StatusCodeResult;

            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
        }
    }
}
