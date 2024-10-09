using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

public class TradeControllerGetAllTest
{
    private readonly Mock<ITradeService> _mockTradeService;
    private readonly Mock<ILogger<TradeController>> _mockLogger;
    private readonly TradeController _controller;

    public TradeControllerGetAllTest()
    {
        _mockTradeService = new Mock<ITradeService>();
        _mockLogger = new Mock<ILogger<TradeController>>();
        _controller = new TradeController(_mockTradeService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfTrades()
    {
        // Arrange
        var mockTrades = new List<TradeDTO>
        {
            new TradeDTO
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
            },
            new TradeDTO
            {
                TradeId = 2,
                Account = "Account2",
                AccountType = "Type2",
                BuyQuantity = 200,
                SellQuantity = 100,
                BuyPrice = 20.5,
                SellPrice = 22.0,
                TradeDate = DateTime.UtcNow,
                TradeSecurity = "Sec2",
                TradeStatus = "Closed",
                Trader = "Trader2",
                Benchmark = "Benchmark2",
                Book = "Book2",
                CreationName = "Creator2",
                CreationDate = DateTime.UtcNow,
                RevisionName = "Revisor2",
                RevisionDate = DateTime.UtcNow,
                DealName = "Deal2",
                DealType = "TypeB",
                SourceListId = "Source2",
                Side = "Sell"
            }
        };

        _mockTradeService.Setup(service => service.GetAll()).ReturnsAsync(mockTrades);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnTrades = Assert.IsType<List<TradeDTO>>(okResult.Value);
        Assert.Equal(2, returnTrades.Count);
    }
}
