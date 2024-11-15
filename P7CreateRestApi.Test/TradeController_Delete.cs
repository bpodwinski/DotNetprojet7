using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    public class TradeControllerDeleteTest
    {
        private readonly Mock<ITradeService> _mockService;
        private readonly Mock<ILogger<TradeController>> _mockLogger;
        private readonly TradeController _controller;

        public TradeControllerDeleteTest()
        {
            _mockService = new Mock<ITradeService>();
            _mockLogger = new Mock<ILogger<TradeController>>();
            _controller = new TradeController(_mockService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Delete_Ok()
        {
            // Arrange
            var trade = new TradeDTO
            {
                TradeId = 1,
                Account = "UpdatedAccount",
                AccountType = "UpdatedType",
                BuyQuantity = 150,
                SellQuantity = 70,
                BuyPrice = 15.5,
                SellPrice = 17.0,
                TradeDate = DateTime.UtcNow,
                TradeSecurity = "UpdatedSec",
                TradeStatus = "UpdatedStatus",
                Trader = "UpdatedTrader",
                Benchmark = "UpdatedBenchmark",
                Book = "UpdatedBook",
                CreationName = "UpdatedCreator",
                CreationDate = DateTime.UtcNow,
                RevisionName = "UpdatedRevisor",
                RevisionDate = DateTime.UtcNow,
                DealName = "UpdatedDeal",
                DealType = "UpdatedTypeA",
                SourceListId = "UpdatedSource",
                Side = "Sell"
            };

            _mockService.Setup(service => service.DeleteById(1)).ReturnsAsync(trade);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_NotFound()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteById(1)).ReturnsAsync((TradeDTO)null);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Trade with ID 1 not found.", notFoundResult.Value);
        }

        /// <summary>
        /// Tests if Delete returns 500 Internal Server Error when an exception occurs.
        /// </summary>
        [Fact]
        public async Task Delete_InternalServerError()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteById(1)).ThrowsAsync(new Exception("Internal error"));

            // Act
            var result = await _controller.Delete(1);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
    }
}
