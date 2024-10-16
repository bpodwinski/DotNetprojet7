using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Delete method in RuleNameController.
    /// </summary>
    public class RuleNameControllerDeleteTest
    {
        private readonly RuleNameController _controller;
        private readonly Mock<IRuleNameService> _mockService;
        private readonly Mock<ILogger<RuleNameController>> _mockLogger;

        public RuleNameControllerDeleteTest()
        {
            _mockService = new Mock<IRuleNameService>();
            _mockLogger = new Mock<ILogger<RuleNameController>>();
            _controller = new RuleNameController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Delete returns NoContentResult when the RuleName is successfully deleted.
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var deletedRuleName = new RuleNameDTO { Id = 1, Name = "Rule1", Description = "Desc1", Json = "{}", Template = "Template1", SqlStr = "SELECT *", SqlPart = "WHERE" };
            _mockService.Setup(service => service.DeleteById(1)).ReturnsAsync(deletedRuleName);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Tests if Delete returns NotFoundResult when the RuleName is not found.
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsNotFoundResult()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteById(1)).ReturnsAsync((RuleNameDTO)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}