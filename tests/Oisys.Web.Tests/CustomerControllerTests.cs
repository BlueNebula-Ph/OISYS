namespace Oisys.Web.Tests
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Oisys.Web.Controllers;
    using Xunit;

    public class CustomerControllerTests
    {
        [Fact]
        public void Index_Should_Return_ViewResult()
        {
            // Arrange
            var customerController = new ManagementController();

            // Act
            var result = customerController.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}
