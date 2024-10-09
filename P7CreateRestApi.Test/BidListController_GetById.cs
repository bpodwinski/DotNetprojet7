using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for GetById method in BidListController.
    /// </summary>
    public class BidListControllerGetTest
    {
        private readonly Mock<IBidListService> _mockBidListService;
        private readonly Mock<ILogger<BidListController>> _mockLogger;
        private readonly BidListController _controller;

        public BidListControllerGetTest()
        {
            _mockBidListService = new Mock<IBidListService>();
            _mockLogger = new Mock<ILogger<BidListController>>();
            _controller = new BidListController(_mockBidListService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if GetById returns OkResult when the BidList is found.
        /// </summary>
        [Fact]
        public async Task GetById_ReturnsOkResult_WhenBidListExists()
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
            _mockBidListService.Setup(service => service.GetById(1)).ReturnsAsync(mockBidList);

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
        public async Task GetById_ReturnsNotFound_WhenBidListDoesNotExist()
        {
            // Arrange
            _mockBidListService.Setup(service => service.GetById(1)).ReturnsAsync((BidListDTO)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("BidList with ID 1 not found.", notFoundResult.Value);
        }
    }
}
