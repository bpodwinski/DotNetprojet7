using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

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
    public async Task Create_ValidTrade_ReturnsCreatedAtAction()
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
}
