using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiabeticWebApp;
using DiabeticWebApp.Controllers;

namespace DiabeticWebApp.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
