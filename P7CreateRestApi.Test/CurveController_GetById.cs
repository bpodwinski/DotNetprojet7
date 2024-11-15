using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for GetById method in CurveController.
    /// </summary>
    public class CurveControllerGetByIdTest
    {
        private readonly CurveController _controller;
        private readonly Mock<ICurvePointService> _mockService;
        private readonly Mock<ILogger<CurveController>> _mockLogger;

        public CurveControllerGetByIdTest()
        {
            _mockService = new Mock<ICurvePointService>();
            _mockLogger = new Mock<ILogger<CurveController>>();
            _controller = new CurveController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if GetById returns OkResult when the CurvePoint is found.
        /// </summary>
        [Fact]
        public async Task GetById_Ok()
        {
            // Arrange
            var curvePoint = new CurvePointDTO { Id = 1, CurveId = 1 };
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(curvePoint);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CurvePointDTO>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        /// <summary>
        /// Tests if GetById returns NotFoundResult when the CurvePoint is not found.
        /// </summary>
        [Fact]
        public async Task GetById_NotFoundResult()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync((CurvePointDTO)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        /// <summary>
        /// Tests if GetById returns 500 Internal Server Error when an exception occurs.
        /// </summary>
        [Fact]
        public async Task GetById_InternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(1)).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}