using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for GetAll method in BidListController.
    /// </summary>
    public class BidListControllerGetAllTest
    {
        private readonly Mock<IBidListService> _mockBidListService;
        private readonly Mock<ILogger<BidListController>> _mockLogger;
        private readonly BidListController _controller;

        public BidListControllerGetAllTest()
        {
            _mockBidListService = new Mock<IBidListService>();
            _mockLogger = new Mock<ILogger<BidListController>>();
            _controller = new BidListController(_mockBidListService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if GetAll returns OkResult with a list of BidListDTOs.
        /// </summary>
        [Fact]
        public async Task GetAll_ReturnsOkResult_WhenBidListsExist()
        {
            // Arrange
            var mockBidList = new List<BidListDTO>
            {
                new() {
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
                },
                new() {
                    BidListId = 2,
                    Account = "Account2",
                    BidType = "Type2",
                    BidQuantity = 150,
                    AskQuantity = 250,
                    Bid = 60.5,
                    Ask = 61.0,
                    Benchmark = "Benchmark2",
                    BidListDate = DateTime.Now,
                    Commentary = "This is another test commentary",
                    BidSecurity = "Security2",
                    BidStatus = "Closed",
                    Trader = "Trader2",
                    Book = "Book2",
                    CreationName = "Creator2",
                    CreationDate = DateTime.Now.AddDays(-1),
                    RevisionName = "Revisor2",
                    RevisionDate = DateTime.Now.AddDays(2),
                    DealName = "Deal2",
                    DealType = "Type2",
                    SourceListId = "SL2",
                    Side = "Sell"
                }
            };

            _mockBidListService.Setup(service => service.GetAll()).ReturnsAsync(mockBidList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<BidListDTO>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetAll_ReturnsInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            _mockBidListService.Setup(service => service.GetAll()).ThrowsAsync(new Exception("Test Exception"));

            // Act
            var result = await _controller.GetAll();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
            Assert.Equal("An internal error occurred.", statusCodeResult.Value);
        }
    }
}
