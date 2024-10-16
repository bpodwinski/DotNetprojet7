using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    public class TradeController_GetByIdTest
    {
        private readonly TradeController _controller;
        private readonly Mock<ITradeService> _mockTradeService;
        private readonly Mock<ILogger<TradeController>> _mockLogger;

        public TradeController_GetByIdTest()
        {
            _mockTradeService = new Mock<ITradeService>();
            _mockLogger = new Mock<ILogger<TradeController>>();
            _controller = new TradeController(_mockTradeService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WithTradeDTO()
        {
            // Arrange
            var tradeId = 1;
            var mockTrade = new TradeDTO
            {
                TradeId = 1,
                Account = "Account1",
                AccountType = "Type1",
                BuyQuantity = 100,
                SellQuantity = 50,
                BuyPrice = 10.5,
                SellPrice = 12.0,
                TradeDate = DateTime.UtcNow,
                TradeSecurity = "Sec1",
                TradeStatus = "Open",
                Trader = "Trader1",
                Benchmark = "Benchmark1",
                Book = "Book1",
                CreationName = "Creator1",
                CreationDate = DateTime.UtcNow,
                RevisionName = "Revisor1",
                RevisionDate = DateTime.UtcNow,
                DealName = "Deal1",
                DealType = "TypeA",
                SourceListId = "Source1",
                Side = "Buy"
            };

            _mockTradeService.Setup(service => service.GetById(tradeId)).ReturnsAsync(mockTrade);

            // Act
            var result = await _controller.GetById(tradeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<TradeDTO>(okResult.Value);
            Assert.Equal(tradeId, returnValue.TradeId);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenTradeDoesNotExist()
        {
            // Arrange
            var tradeId = 1;
            _mockTradeService.Setup(service => service.GetById(tradeId)).ReturnsAsync((TradeDTO)null);

            // Act
            var result = await _controller.GetById(tradeId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Trade with ID {tradeId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task GetById_Returns500_WhenExceptionIsThrown()
        {
            // Arrange
            var tradeId = 1;
            _mockTradeService.Setup(service => service.GetById(tradeId)).ThrowsAsync(new System.Exception());

            // Act
            var result = await _controller.GetById(tradeId);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}