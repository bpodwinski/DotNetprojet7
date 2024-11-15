using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for GetById method in BidListController.
    /// </summary>
    public class BidListControllerGetByIdTest
    {
        private readonly Mock<IBidListService> _mockService;
        private readonly Mock<ILogger<BidListController>> _mockLogger;
        private readonly BidListController _controller;

        public BidListControllerGetByIdTest()
        {
            _mockService = new Mock<IBidListService>();
            _mockLogger = new Mock<ILogger<BidListController>>();
            _controller = new BidListController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if GetById returns OkResult when the BidList is found.
        /// </summary>
        [Fact]
        public async Task GetById_Ok()
        {
            // Arrange
            var mockBidList = new BidListDTO {
                BidListId = 1,
                Account = "Account1",
                BidType = "Type1",
                BidQuantity = 100,
                AskQuantity = 200,
                Bid = 50.5,
                Ask = 51.0,
                Benchmark = "Benchmark1",
                BidListDate = DateTime.Now,
                Commentary = "This is a test commentary",
                BidSecurity = "Security1",
                BidStatus = "Open",
                Trader = "Trader1",
                Book = "Book1",
                CreationName = "Creator1",
                CreationDate = DateTime.Now,
                RevisionName = "Revisor1",
                RevisionDate = DateTime.Now.AddDays(1),
                DealName = "Deal1",
                DealType = "Type1",
                SourceListId = "SL1",
                Side = "Buy"
            };
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync(mockBidList);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BidListDTO>(okResult.Value);
            Assert.Equal(1, returnValue.BidListId);
        }

        /// <summary>
        /// Tests if GetById returns NotFoundResult when the BidList is not found.
        /// </summary>
        [Fact]
        public async Task GetById_NotFound()
        {
            // Arrange
            _mockService.Setup(service => service.GetById(1)).ReturnsAsync((BidListDTO)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("BidList with ID 1 not found.", notFoundResult.Value);
        }

        /// <summary>
        /// Tests if GetById returns 500 Internal Server Error when an exception occurs.
        /// </summary>
        [Fact]
        public async Task GetById_InternalServerError()
        {
            // Arrange
            int bidListId = 1;
            _mockService.Setup(service => service.GetById(bidListId)).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _controller.GetById(bidListId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
