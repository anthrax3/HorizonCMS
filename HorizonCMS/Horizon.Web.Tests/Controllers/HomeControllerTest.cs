using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Horizon.Web.Controllers;

namespace Horizon.Web.Tests.Controllers
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
            ViewResult result = controller.Index("Home",null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
