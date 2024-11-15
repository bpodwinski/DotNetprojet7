using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Create method in CurveController.
    /// </summary>
    public class CurveControllerCreateTest
    {
        private readonly CurveController _controller;
        private readonly Mock<ICurvePointService> _mockService;
        private readonly Mock<ILogger<CurveController>> _mockLogger;

        public CurveControllerCreateTest()
        {
            _mockService = new Mock<ICurvePointService>();
            _mockLogger = new Mock<ILogger<CurveController>>();
            _controller = new CurveController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Create returns CreatedAtActionResult when a new CurvePoint is created.
        /// </summary>
        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var newCurvePoint = new CurvePointDTO { CurveId = 1 };
            _mockService.Setup(service => service.Create(It.IsAny<CurvePointDTO>())).ReturnsAsync(newCurvePoint);

            // Act
            var result = await _controller.Create(newCurvePoint);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<CurvePointDTO>(createdResult.Value);
            Assert.Equal(newCurvePoint.Id, returnValue.Id);
        }

        /// <summary>
        /// Tests if Create returns BadRequestResult when the model state is invalid.
        /// </summary>
        [Fact]
        public async Task Create_ModelStateInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("CurveId", "The CurveId field is required.");

            // Act
            var result = await _controller.Create(new CurvePointDTO { CurveId = 1 });


            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Tests if Create returns 500 Internal Server Error when an exception is thrown in the service.
        /// </summary>
        [Fact]
        public async Task Create_InternalServerError()
        {
            // Arrange
            var newCurvePoint = new CurvePointDTO { CurveId = 1 };
            _mockService.Setup(service => service.Create(It.IsAny<CurvePointDTO>())).ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.Create(newCurvePoint);

            // Assert
            var serverErrorResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, serverErrorResult.StatusCode);
        }
    }
}