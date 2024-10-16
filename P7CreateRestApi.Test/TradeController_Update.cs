using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    public class TradeControllerUpdateTest
    {
        private readonly Mock<ITradeService> _mockTradeService;
        private readonly Mock<ILogger<TradeController>> _mockLogger;
        private readonly TradeController _controller;

        public TradeControllerUpdateTest()
        {
            _mockTradeService = new Mock<ITradeService>();
            _mockLogger = new Mock<ILogger<TradeController>>();
            _controller = new TradeController(_mockTradeService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Update_ValidTrade_ReturnsOkResult()
        {
            // Arrange
            var updatedTrade = new TradeDTO
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

            _mockTradeService.Setup(service => service.Update(1, It.IsAny<TradeDTO>())).ReturnsAsync(updatedTrade);

            // Act
            var result = await _controller.Update(1, updatedTrade);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnTrade = Assert.IsType<TradeDTO>(okResult.Value);
            Assert.Equal(updatedTrade.TradeId, returnTrade.TradeId);
        }

        [Fact]
        public async Task Update_TradeNotFound_ReturnsNotFound()
        {
            // Arrange
            _mockTradeService.Setup(service => service.Update(1, It.IsAny<TradeDTO>())).ReturnsAsync((TradeDTO)null);

            // Act
            var result = await _controller.Update(1, new TradeDTO
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
            });

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Trade with ID 1 not found.", notFoundResult.Value);
        }
    }
}