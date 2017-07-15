namespace Oisys.Web.Tests
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Oisys.Web.Controllers;
    using Xunit;

    public class InventoryControllerTests
    {
        [Fact]
        public void Index_Should_Return_ViewResult()
        {
            // Arrange
            var inventoryController = new InventoryController();

            // Act
            var result = inventoryController.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}