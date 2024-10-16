using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for GetAll method in RatingController.
    /// </summary>
    public class RatingControllerGetAllTest
    {
        private readonly RatingController _controller;
        private readonly Mock<IRatingService> _mockService;
        private readonly Mock<ILogger<RatingController>> _mockLogger;

        public RatingControllerGetAllTest()
        {
            _mockService = new Mock<IRatingService>();
            _mockLogger = new Mock<ILogger<RatingController>>();
            _controller = new RatingController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if GetAll returns OkResult with a list of RatingDTOs.
        /// </summary>
        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfRatingDTOs()
        {
            // Arrange
            var ratings = new List<RatingDTO>
        {
            new() { Id = 1, MoodysRating = "A1", SandPRating = "AA+", FitchRating = "A+", OrderNumber = 1 },
            new() { Id = 2, MoodysRating = "B1", SandPRating = "BB+", FitchRating = "B+", OrderNumber = 2 }
        };
            _mockService.Setup(service => service.GetAll()).ReturnsAsync(ratings);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<RatingDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
    }
}