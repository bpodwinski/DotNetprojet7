using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

/// <summary>
/// Unit test for GetAll method in CurveController.
/// </summary>
public class CurveControllerGetAllTest
{
    private readonly CurveController _controller;
    private readonly Mock<ICurvePointService> _mockService;
    private readonly Mock<ILogger<CurveController>> _mockLogger;

    public CurveControllerGetAllTest()
    {
        _mockService = new Mock<ICurvePointService>();
        _mockLogger = new Mock<ILogger<CurveController>>();
        _controller = new CurveController(_mockService.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Tests if GetAll returns OkResult with a list of CurvePoints.
    /// </summary>
    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfCurvePoints()
    {
        // Arrange
        var curvePoints = new List<CurvePointDTO>
        {
            new() { Id = 1, CurveId = 1 },
            new() { Id = 2, CurveId = 2 }
        };
        _mockService.Setup(service => service.GetAll()).ReturnsAsync(curvePoints);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<CurvePointDTO>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
}
