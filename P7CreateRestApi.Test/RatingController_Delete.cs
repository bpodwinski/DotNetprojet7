using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Delete method in RatingController.
    /// </summary>
    public class RatingControllerDeleteTest
    {
        private readonly RatingController _controller;
        private readonly Mock<IRatingService> _mockService;
        private readonly Mock<ILogger<RatingController>> _mockLogger;

        public RatingControllerDeleteTest()
        {
            _mockService = new Mock<IRatingService>();
            _mockLogger = new Mock<ILogger<RatingController>>();
            _controller = new RatingController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Delete returns NoContentResult when the Rating is successfully deleted.
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsNoContentResult()
        {
            // Arrange
            var deletedRating = new RatingDTO { Id = 1, MoodysRating = "A1", SandPRating = "AA+", FitchRating = "A+", OrderNumber = 1 };
            _mockService.Setup(service => service.Delete(1)).ReturnsAsync(deletedRating);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Tests if Delete returns NotFoundResult when the Rating is not found.
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsNotFoundResult()
        {
            // Arrange
            _mockService.Setup(service => service.Delete(1)).ReturnsAsync((RatingDTO)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}