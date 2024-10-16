using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Update method in RuleNameController.
    /// </summary>
    public class RuleNameControllerUpdateTest
    {
        private readonly RuleNameController _controller;
        private readonly Mock<IRuleNameService> _mockService;
        private readonly Mock<ILogger<RuleNameController>> _mockLogger;

        public RuleNameControllerUpdateTest()
        {
            _mockService = new Mock<IRuleNameService>();
            _mockLogger = new Mock<ILogger<RuleNameController>>();
            _controller = new RuleNameController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Update returns OkResult when the RuleName is successfully updated.
        /// </summary>
        [Fact]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var updatedRuleName = new RuleNameDTO { Id = 1, Name = "UpdatedRule", Description = "UpdatedDesc", Json = "{}", Template = "TemplateUpdated", SqlStr = "SELECT *", SqlPart = "WHERE" };
            _mockService.Setup(service => service.Update(1, It.IsAny<RuleNameDTO>())).ReturnsAsync(updatedRuleName);

            // Act
            var result = await _controller.Update(1, updatedRuleName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<RuleNameDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        /// <summary>
        /// Tests if Update returns NotFoundResult when the RuleName is not found.
        /// </summary>
        [Fact]
        public async Task Update_ReturnsNotFoundResult()
        {
            // Arrange
            _mockService.Setup(service => service.Update(1, It.IsAny<RuleNameDTO>())).ReturnsAsync((RuleNameDTO)null);

            // Act
            var result = await _controller.Update(1, new RuleNameDTO { Id = 1, Name = "UpdatedRule", Description = "UpdatedDesc", Json = "{}", Template = "TemplateUpdated", SqlStr = "SELECT *", SqlPart = "WHERE" });

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}