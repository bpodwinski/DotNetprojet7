using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;
public class TradeControllerDeleteTest
{
    private readonly Mock<ITradeService> _mockTradeService;
    private readonly Mock<ILogger<TradeController>> _mockLogger;
    private readonly TradeController _controller;

    public TradeControllerDeleteTest()
    {
        _mockTradeService = new Mock<ITradeService>();
        _mockLogger = new Mock<ILogger<TradeController>>();
        _controller = new TradeController(_mockTradeService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task Delete_ValidTrade_ReturnsNoContentResult()
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

        _mockTradeService.Setup(service => service.Delete(1)).ReturnsAsync(trade);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_TradeNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        _mockTradeService.Setup(service => service.Delete(1)).ReturnsAsync((TradeDTO)null);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Trade with ID 1 not found.", notFoundResult.Value);
    }
}
