using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for GetById method in RuleNameController.
    /// </summary>
    public class RuleNameControllerGetByIdTest
    {
        private readonly RuleNameController _controller;
        private readonly Mock<IRuleNameService> _mockService;
        private readonly Mock<ILogger<RuleNameController>> _mockLogger;

        public RuleNameControllerGetByIdTest()
        {
            _mockService = new Mock<IRuleNameService>();
            _mockLogger = new Mock<ILogger<RuleNameController>>();
            _controller = new RuleNameController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if GetById returns OkResult when the RuleName is found.
        /// </summary>
        [Fact]
        public async Task GetById_ReturnsOkResult_WithRuleNameDTO()
        {
            // Arrange
            var ruleName = new RuleNameDTO { Id = 1, Name = "Rule1", Description = "Desc1", Json = "{}", Template = "Template1", SqlStr = "SELECT *", SqlPart = "WHERE" };
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(ruleName);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<RuleNameDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        /// <summary>
        /// Tests if GetById returns NotFoundResult when the RuleName is not found.
        /// </summary>
        [Fact]
        public async Task GetById_ReturnsNotFoundResult()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync((RuleNameDTO)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}