using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

/// <summary>
/// Unit test for Delete method in CurveController.
/// </summary>
public class CurveControllerDeleteTest
{
    private readonly CurveController _controller;
    private readonly Mock<ICurvePointService> _mockService;
    private readonly Mock<ILogger<CurveController>> _mockLogger;

    public CurveControllerDeleteTest()
    {
        _mockService = new Mock<ICurvePointService>();
        _mockLogger = new Mock<ILogger<CurveController>>();
        _controller = new CurveController(_mockService.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Tests if Delete returns NoContentResult when the CurvePoint is successfully deleted.
    /// </summary>
    [Fact]
    public async Task Delete_ReturnsNoContentResult()
    {
        // Arrange
        var curvePoint = new CurvePointDTO { Id = 1, CurveId = 1 };
        _mockService.Setup(service => service.DeleteById(1)).ReturnsAsync(curvePoint);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    /// <summary>
    /// Tests if Delete returns NotFoundResult when the CurvePoint is not found.
    /// </summary>
    [Fact]
    public async Task Delete_ReturnsNotFoundResult()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteById(1)).ReturnsAsync((CurvePointDTO)null);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
