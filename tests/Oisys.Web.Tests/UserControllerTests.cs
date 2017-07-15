namespace Oisys.Web.Tests
{
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Oisys.Web.Controllers;
    using Xunit;

    public class UserControllerTests
    {
        [Fact]
        public void Index_Should_Return_ViewResult()
        {
            // Arrange
            var userController = new UserController();

            // Act
            var result = userController.Index();

            // Assert
            result.Should().BeOfType<ViewResult>();
        }
    }
}