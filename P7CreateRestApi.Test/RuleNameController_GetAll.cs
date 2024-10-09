using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

/// <summary>
/// Unit test for GetAll method in RuleNameController.
/// </summary>
public class RuleNameControllerGetAllTest
{
    private readonly RuleNameController _controller;
    private readonly Mock<IRuleNameService> _mockService;
    private readonly Mock<ILogger<RuleNameController>> _mockLogger;

    public RuleNameControllerGetAllTest()
    {
        _mockService = new Mock<IRuleNameService>();
        _mockLogger = new Mock<ILogger<RuleNameController>>();
        _controller = new RuleNameController(_mockService.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Tests if GetAll returns OkResult with a list of RuleNameDTOs.
    /// </summary>
    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfRuleNameDTOs()
    {
        // Arrange
        var ruleNames = new List<RuleNameDTO>
        {
            new() { Id = 1, Name = "Rule1", Description = "Desc1", Json = "{}", Template = "Template1", SqlStr = "SELECT *", SqlPart = "WHERE" },
            new() { Id = 2, Name = "Rule2", Description = "Desc2", Json = "{}", Template = "Template2", SqlStr = "SELECT *", SqlPart = "WHERE" }
        };
        _mockService.Setup(service => service.GetAll()).ReturnsAsync(ruleNames);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<RuleNameDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
}
