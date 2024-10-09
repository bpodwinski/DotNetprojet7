using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

/// <summary>
/// Unit test for Update method in CurveController.
/// </summary>
public class CurveControllerUpdateTest
{
    private readonly CurveController _controller;
    private readonly Mock<ICurvePointService> _mockService;
    private readonly Mock<ILogger<CurveController>> _mockLogger;

    public CurveControllerUpdateTest()
    {
        _mockService = new Mock<ICurvePointService>();
        _mockLogger = new Mock<ILogger<CurveController>>();
        _controller = new CurveController(_mockService.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Tests if Update returns OkResult when the CurvePoint is successfully updated.
    /// </summary>
    [Fact]
    public async Task Update_ReturnsOkResult()
    {
        // Arrange
        var updatedCurvePoint = new CurvePointDTO { Id = 1, CurveId = 1 };
        _mockService.Setup(service => service.Update(1, It.IsAny<CurvePointDTO>())).ReturnsAsync(updatedCurvePoint);

        // Act
        var result = await _controller.Update(1, updatedCurvePoint);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<CurvePointDTO>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    /// <summary>
    /// Tests if Update returns NotFoundResult when the CurvePoint is not found.
    /// </summary>
    [Fact]
    public async Task Update_ReturnsNotFoundResult()
    {
        // Arrange
        _mockService.Setup(service => service.Update(1, It.IsAny<CurvePointDTO>())).ReturnsAsync((CurvePointDTO)null);

        // Act
        var result = await _controller.Update(1, new CurvePointDTO());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
