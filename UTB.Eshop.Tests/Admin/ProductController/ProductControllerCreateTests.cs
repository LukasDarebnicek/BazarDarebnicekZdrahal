using Xunit;
using UTB.Eshop.Web.Areas.Admin.Controllers;
using UTB.Eshop.Application.Abstraction;
using Moq;
using Microsoft.AspNetCore.Mvc;
using UTB.Eshop.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using UTB.Eshop.Infrastructure.Identity;

namespace UTB.Eshop.Tests.Admin.ProductController
{
    public class ProductControllerTests
    {
        [Fact]
        public void Delete_ProductExists_ReturnsRedirectToActionResult()
        {
            // Arrange
            int productId = 1;
            Mock<IProductService> productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(productService => productService.Delete(productId))
                .Returns(true); // Simuluje úspěšné smazání

            Mock<UserManager<User>> userManagerMock = GetUserManagerMock();
            Mock<IWebHostEnvironment> hostingEnvironmentMock = new Mock<IWebHostEnvironment>();

            var productController = new Web.Areas.Admin.Controllers.ProductController(
                productServiceMock.Object, userManagerMock.Object, hostingEnvironmentMock.Object
            );

            // Act
            var actionResult = productController.Delete(productId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.Equal(nameof(Web.Areas.Admin.Controllers.ProductController.Index), redirectToActionResult.ActionName);

            productServiceMock.Verify(productService => productService.Delete(productId), Times.Once);
        }

        [Fact]
        public void Delete_ProductNotExists_ReturnsNotFoundResult()
        {
            // Arrange
            int productId = 1;
            Mock<IProductService> productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(productService => productService.Delete(productId))
                .Returns(false); // Simuluje neúspěšné smazání

            Mock<UserManager<User>> userManagerMock = GetUserManagerMock();
            Mock<IWebHostEnvironment> hostingEnvironmentMock = new Mock<IWebHostEnvironment>();

            var productController = new Web.Areas.Admin.Controllers.ProductController(
                productServiceMock.Object, userManagerMock.Object, hostingEnvironmentMock.Object
            );

            // Act
            var actionResult = productController.Delete(productId);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);

            productServiceMock.Verify(productService => productService.Delete(productId), Times.Once);
        }

        private Mock<UserManager<User>> GetUserManagerMock()
        {
            var store = new Mock<IUserStore<User>>();
            return new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
        }
    }
}
