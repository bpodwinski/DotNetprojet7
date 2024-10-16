using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Create method in RatingController.
    /// </summary>
    public class RatingControllerCreateTest
    {
        private readonly RatingController _controller;
        private readonly Mock<IRatingService> _mockService;
        private readonly Mock<ILogger<RatingController>> _mockLogger;

        public RatingControllerCreateTest()
        {
            _mockService = new Mock<IRatingService>();
            _mockLogger = new Mock<ILogger<RatingController>>();
            _controller = new RatingController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Create returns CreatedAtActionResult when a new Rating is created.
        /// </summary>
        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var newRating = new RatingDTO { Id = 1, MoodysRating = "A1", SandPRating = "AA+", FitchRating = "A+", OrderNumber = 1 };
            _mockService.Setup(service => service.Create(It.IsAny<RatingDTO>())).ReturnsAsync(newRating);

            // Act
            var result = await _controller.Create(newRating);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<RatingDTO>(createdResult.Value);
            Assert.Equal(newRating.Id, returnValue.Id);
        }

        /// <summary>
        /// Tests if Create returns BadRequestResult when the model state is invalid.
        /// </summary>
        [Fact]
        public async Task Create_ReturnsBadRequestResult_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("MoodysRating", "The MoodysRating field is required.");

            // Act
            var result = await _controller.Create(new RatingDTO());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}