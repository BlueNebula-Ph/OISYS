namespace Oisys.Web.Tests
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Oisys.Web.Controllers;
    using Xunit;

    public class DeliveryControllerTests
    {
        [Fact]
        public void Index_Should_Return_ViewResult()
        {
            // Arrange
            var deliveryController = new DeliveryController();

            // Act
            var result = deliveryController.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}