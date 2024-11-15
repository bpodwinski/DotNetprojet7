using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    public class TradeControllerCreateTest
    {
        private readonly Mock<ITradeService> _mockTradeService;
        private readonly Mock<ILogger<TradeController>> _mockLogger;
        private readonly TradeController _controller;

        public TradeControllerCreateTest()
        {
            _mockTradeService = new Mock<ITradeService>();
            _mockLogger = new Mock<ILogger<TradeController>>();
            _controller = new TradeController(_mockTradeService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Create_Ok()
        {
            // Arrange
            var newTrade = new TradeDTO
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

            _mockTradeService.Setup(service => service.Create(It.IsAny<TradeDTO>())).ReturnsAsync(newTrade);

            // Act
            var result = await _controller.Create(newTrade);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnTrade = Assert.IsType<TradeDTO>(createdAtActionResult.Value);
            Assert.Equal(newTrade.TradeId, returnTrade.TradeId);
        }

        [Fact]
        public async Task Create_BadRequest()
        {
            // Arrange
            var newTrade = new TradeDTO
            {
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

            // Simulate the service returning null, indicating a failure to create the trade
            _mockTradeService.Setup(service => service.Create(It.IsAny<TradeDTO>())).ReturnsAsync((TradeDTO)null);

            // Act
            var result = await _controller.Create(newTrade);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Trade could not be created.", badRequestResult.Value);
        }

        [Fact]
        public async Task Create_ModelStateInvalid()
        {
            // Arrange
            var newTrade = new TradeDTO
            {
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

            // Simulate an invalid model state by adding an error
            _controller.ModelState.AddModelError("Account", "The Account field is required.");

            // Act
            var result = await _controller.Create(newTrade);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var serializableError = Assert.IsType<SerializableError>(badRequestResult.Value);

            // Check that the error message is as expected
            Assert.True(serializableError.ContainsKey("Account"));
            Assert.Equal("The Account field is required.", ((string[])serializableError["Account"])[0]);
        }

        [Fact]
        public async Task Create_InternalServerError()
        {
            // Arrange
            var newTrade = new TradeDTO
            {
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
            _mockTradeService.Setup(service => service.Create(It.IsAny<TradeDTO>())).ThrowsAsync(new Exception("An error occurred"));

            // Act
            var result = await _controller.Create(newTrade);

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
