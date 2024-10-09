using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

/// <summary>
/// Unit test for Update method in RatingController.
/// </summary>
public class RatingControllerUpdateTest
{
    private readonly RatingController _controller;
    private readonly Mock<IRatingService> _mockService;
    private readonly Mock<ILogger<RatingController>> _mockLogger;

    public RatingControllerUpdateTest()
    {
        _mockService = new Mock<IRatingService>();
        _mockLogger = new Mock<ILogger<RatingController>>();
        _controller = new RatingController(_mockService.Object, _mockLogger.Object);
    }

    /// <summary>
    /// Tests if Update returns OkResult when the Rating is successfully updated.
    /// </summary>
    [Fact]
    public async Task Update_ReturnsOkResult()
    {
        // Arrange
        var updatedRating = new RatingDTO { Id = 1, MoodysRating = "A1", SandPRating = "AA+", FitchRating = "A+", OrderNumber = 1 };
        _mockService.Setup(service => service.Update(1, It.IsAny<RatingDTO>())).ReturnsAsync(updatedRating);

        // Act
        var result = await _controller.Update(1, updatedRating);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<RatingDTO>(okResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    /// <summary>
    /// Tests if Update returns NotFoundResult when the Rating is not found.
    /// </summary>
    [Fact]
    public async Task Update_ReturnsNotFoundResult()
    {
        // Arrange
        _mockService.Setup(service => service.Update(1, It.IsAny<RatingDTO>())).ReturnsAsync((RatingDTO)null);

        // Act
        var result = await _controller.Update(1, new RatingDTO());

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
}
