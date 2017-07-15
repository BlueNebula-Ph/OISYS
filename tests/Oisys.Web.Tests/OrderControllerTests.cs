namespace Oisys.Web.Tests
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Oisys.Web.Controllers;
    using Xunit;

    public class OrderControllerTests
    {
        [Fact]
        public void Index_Should_Return_ViewResult()
        {
            // Arrange
            var orderController = new OrderController();

            // Act
            var result = orderController.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}