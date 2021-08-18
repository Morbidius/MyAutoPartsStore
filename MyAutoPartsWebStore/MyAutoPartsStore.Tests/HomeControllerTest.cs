namespace MyAutoPartsStore.Tests
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using MyAutoPartsStore.Services.ProductServices;
    using MyAutoPartsWebStore.Web.Controllers;
    using Xunit;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
        {
            //Arrange
            var homeController = new HomeController(Mock.Of<IProductService>(), null);

            //Act
            var result = homeController.Error();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }
    }
}
