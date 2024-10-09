using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Delete method in BidListController.
    /// </summary>
    public class BidListControlleDeleteTest
    {
        private readonly Mock<IBidListService> _mockBidListService;
        private readonly Mock<ILogger<BidListController>> _mockLogger;
        private readonly BidListController _controller;

        public BidListControlleDeleteTest()
        {
            _mockBidListService = new Mock<IBidListService>();
            _mockLogger = new Mock<ILogger<BidListController>>();
            _controller = new BidListController(_mockBidListService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Delete returns NoContentResult when the BidList is successfully deleted.
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsNoContent_WhenBidListIsDeleted()
        {
            // Arrange
            _mockBidListService.Setup(service => service.DeleteById(1)).ReturnsAsync(new BidListDTO {
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
            });

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        /// <summary>
        /// Tests if Delete returns NotFoundResult when the BidList is not found.
        /// </summary>
        [Fact]
        public async Task Delete_ReturnsNotFound_WhenBidListDoesNotExist()
        {
            // Arrange
            _mockBidListService.Setup(service => service.DeleteById(1)).ReturnsAsync((BidListDTO)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("BidList with ID 1 not found.", notFoundResult.Value);
        }
    }
}
