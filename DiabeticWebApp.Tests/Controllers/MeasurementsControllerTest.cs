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
using DiabeticWebApp.Tests.Builders;
using DiabeticWebApp.Tests.TestObjects;
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

        [TestInitialize]
        public void SetUp()
        {
            _mockService = new Mock<IMeasurementsService>();
            _controller = new MeasurementsController(_mockService.Object);
        }

        [TestMethod]
        public void UT_M_01_Given_MeasurementInDatabase_When_GetThisMeasurement_Then_ShouldReturnThisMeasurementWithOkCode()
        {
            //Arrange
            var measurement = TestMeasurements.DefaultMeasurement().Build();

            _mockService.Setup(x => x.GetMeasurement(It.IsAny<string>(), measurement.Id))
                .Returns(measurement);

            //Act
            var response = _controller.GetMeasurement(measurement.Id);
            var contentResult = response as OkNegotiatedContentResult<MeasurementDto>;         

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(measurement.Id, contentResult.Content.Id);
            Assert.AreEqual(measurement.Date, contentResult.Content.Date);
            Assert.AreEqual(measurement.Description, contentResult.Content.Description);
            Assert.AreEqual(measurement.Result, contentResult.Content.Result);
        }

        [TestMethod]
        public void UT_M_02_Given_MeasurementsInDatabase_When_GetAllUserMeasurements_Then_ShouldReturnAllUserMeasurementsWtihOkCode()
        {
            //Arrange
            var measurementsList = GetListOfFiveMeasurementsWithRandomData();
            _mockService.Setup(x => x.GetMeasurements(It.IsAny<string>()))
                .Returns(measurementsList);

            //Act
            var response = _controller.GetMeasurements();
            var contentResult = response as OkNegotiatedContentResult<List<MeasurementDto>>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(measurementsList, contentResult.Content);
        }

        [TestMethod]
        public void UT_M_03_Given_NoMeasurementWithThisIdInDatabase_When_GetMeasurementWithThisId_Then_ShouldReturnNotFoundCode()
        {
            //Arrange
            int idOfNotExistingMeasurement = 1;
            _mockService.Setup(x => x.GetMeasurement(It.IsAny<string>(), idOfNotExistingMeasurement))
                .Returns(() => null);

            //Act
            var response = _controller.GetMeasurement(idOfNotExistingMeasurement);
            var contentResult = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
        }

        [TestMethod]
        public void UT_M_04_Given_ValidMeasurement_When_PostThisMeasurement_Then_ShouldReturnOkCode()
        {
            //Arrange
            var measurement = TestMeasurements.DefaultMeasurement().Build();

            _mockService.Setup(x => x.AddMeasurement(It.IsAny<string>(), measurement));

            //Act
            var response = _controller.PostMeasurement(measurement);


            //Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void UT_M_06_Given_InvalidMeasurementWithoutResult_When_PostThisMeasurement_Then_ShouldReturnInvalidModelState()
        {
            //Arrange
            var measurement = TestMeasurements.DefaultMeasurement();
            var measurementWithoutResult = measurement.WithNoResult().Build();

            var validationContext = new ValidationContext(measurementWithoutResult, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(measurementWithoutResult, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act
            var response = _controller.PostMeasurement(measurementWithoutResult);

            //Assert
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void UT_M_07_Given_InvalidMeasurementWithNegativeResult_When_PostThisMeasurement_Then_ShouldReturnInvalidModelState()
        {
            //Arrange
            var measurement = TestMeasurements.DefaultMeasurement();
            var measurementWithNegativeResult = measurement.WithNegativeResult().Build();

            var validationContext = new ValidationContext(measurementWithNegativeResult, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(measurementWithNegativeResult, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                _controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

            //Act
            var response = _controller.PostMeasurement(measurementWithNegativeResult);

            //Assert
            Assert.IsInstanceOfType(response, typeof(InvalidModelStateResult));
        }

        [TestMethod]
        public void UT_M_08_Given_MeasurementInDatabase_When_PutAnotherMeasurementOnTheIdOfThisMeasurement_Then_ShouldReturnOkCode()
        {
            //Arrange
            var measurement = TestMeasurements.DefaultMeasurement().Build();
            var newMeasurement = TestMeasurements.RandomMeasurement().Build();

            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(true);
            _mockService.Setup(x => x.UpdateMeasurement(It.IsAny<string>(), measurement));
            _controller.PostMeasurement(measurement);

            //Act
            var response = _controller.PutMeasurement(measurement.Id, newMeasurement);

            //Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void UT_M_09_Given_NoMeasurementInDatabaseWithThisId_When_PutAnotherMeasurementOnThisId_Then_ShouldReturnNotFoundCode()
        {
            //Arrange
            var measurement = TestMeasurements.DefaultMeasurement().Build();
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(false);
            _mockService.Setup(x => x.UpdateMeasurement(It.IsAny<string>(), measurement));

            //Act
            var response = _controller.PutMeasurement(measurement.Id, measurement);
            var contentResult = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
        }

        [TestMethod]
        public void UT_M_10_Given_MeasurementInDatabase_When_DeleteThisMeasurement_Then_ShouldReturnOkCode()
        {
            //Arrange
            var idOfExistingMeasurement = 1;
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), idOfExistingMeasurement))
                .Returns(true);
            _mockService.Setup(x => x.DeleteMeasurement(It.IsAny<string>(), idOfExistingMeasurement));

            //act
            var response = _controller.DeleteMeasurement(idOfExistingMeasurement);

            //Assert
            Assert.IsInstanceOfType(response, typeof(OkResult));
        }

        [TestMethod]
        public void UT_M_11_Given_NoMeasurementInDatabaseWithThisId_When_DeleteMeasurementWithThisId_Then_ShouldReturnNotFoundCode()
        {
            //Arrange
            var idOfNotExistingMeasurement = 1;
            _mockService.Setup(x => x.DoesMeasurementExists(It.IsAny<string>(), idOfNotExistingMeasurement))
                .Returns(false);
            _mockService.Setup(x => x.DeleteMeasurement(It.IsAny<string>(), idOfNotExistingMeasurement));

            //Act
            var response = _controller.DeleteMeasurement(1);
            var contentResult = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(HttpStatusCode.NotFound, contentResult.StatusCode);
        }

        private List<MeasurementDto> GetListOfFiveMeasurementsWithRandomData()
        {
            List<MeasurementDto> measurementsList = new List<MeasurementDto>();

            for (int i = 0; i < 5; i++)
            {
                measurementsList.Add(TestMeasurements.RandomMeasurement().WithId(i).Build());
            }

            return measurementsList;
        }
    }
}
