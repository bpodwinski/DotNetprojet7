using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Create method in RuleNameController.
    /// </summary>
    public class RuleNameControllerCreateTest
    {
        private readonly RuleNameController _controller;
        private readonly Mock<IRuleNameService> _mockService;
        private readonly Mock<ILogger<RuleNameController>> _mockLogger;

        public RuleNameControllerCreateTest()
        {
            _mockService = new Mock<IRuleNameService>();
            _mockLogger = new Mock<ILogger<RuleNameController>>();
            _controller = new RuleNameController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Create returns CreatedAtActionResult when a new RuleName is created.
        /// </summary>
        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var newRuleName = new RuleNameDTO { Id = 1, Name = "Rule1", Description = "Desc1", Json = "{}", Template = "Template1", SqlStr = "SELECT *", SqlPart = "WHERE" };
            _mockService.Setup(service => service.Create(It.IsAny<RuleNameDTO>())).ReturnsAsync(newRuleName);

            // Act
            var result = await _controller.Create(newRuleName);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<RuleNameDTO>(createdResult.Value);
            Assert.Equal(newRuleName.Id, returnValue.Id);
        }

        /// <summary>
        /// Tests if Create returns BadRequestResult when the model state is invalid.
        /// </summary>
        [Fact]
        public async Task Create_ModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Name", "The Name field is required.");

            // Act
            var result = await _controller.Create(new RuleNameDTO { Name = "Rule1", Description = "Desc1", Json = "{}", Template = "Template1", SqlStr = "SELECT *", SqlPart = "WHERE" });


            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests if Create returns 500 Internal Server Error when an exception is thrown in the service.
        /// </summary>
        [Fact]
        public async Task Create_InternalServerError()
        {
            // Arrange
            var newRuleName = new RuleNameDTO { Id = 1, Name = "Rule1", Description = "Desc1", Json = "{}", Template = "Template1", SqlStr = "SELECT *", SqlPart = "WHERE" };
            _mockService.Setup(service => service.Create(It.IsAny<RuleNameDTO>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Create(newRuleName);

            // Assert
            var serverErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, serverErrorResult.StatusCode);
        }
    }
}
