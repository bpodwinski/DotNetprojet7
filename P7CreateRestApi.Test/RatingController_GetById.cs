using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

/// <summary>
/// Unit test for GetById method in RatingController.
/// </summary>
public class RatingControllerGetByIdTest
{
    private readonly RatingController _controller;
    private readonly Mock<IRatingService> _mockService;
    private readonly Mock<ILogger<RatingController>> _mockLogger;

    public RatingControllerGetByIdTest()
    {
        _mockService = new Mock<IRatingService>();
        _mockLogger = new Mock<ILogger<RatingController>>();
        _controller = new RatingController(_mockService.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Tests if GetById returns OkResult when the Rating is found.
    /// </summary>
    [Fact]
    public async Task GetById_ReturnsOkResult_WithRatingDTO()
    {
        // Arrange
        var rating = new RatingDTO { Id = 1, MoodysRating = "A1", SandPRating = "AA+", FitchRating = "A+", OrderNumber = 1 };
        _mockService.Setup(service => service.GetById(1)).ReturnsAsync(rating);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<RatingDTO>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    /// <summary>
    /// Tests if GetById returns NotFoundResult when the Rating is not found.
    /// </summary>
    [Fact]
    public async Task GetById_ReturnsNotFoundResult()
    {
        // Arrange
        _mockService.Setup(service => service.GetById(1)).ReturnsAsync((RatingDTO)null);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
