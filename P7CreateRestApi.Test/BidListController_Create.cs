using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Create method in BidListController.
    /// </summary>
    public class BidListControlleCreateTest
    {
        private readonly Mock<IBidListService> _mockBidListService;
        private readonly Mock<ILogger<BidListController>> _mockLogger;
        private readonly BidListController _controller;

        public BidListControlleCreateTest()
        {
            _mockBidListService = new Mock<IBidListService>();
            _mockLogger = new Mock<ILogger<BidListController>>();
            _controller = new BidListController(_mockBidListService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Create returns CreatedAtActionResult when a new BidList is created.
        /// </summary>
        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult_WhenBidListIsCreated()
        {
            // Arrange
            var newBidList = new BidListDTO {
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
            _mockBidListService.Setup(service => service.Create(It.IsAny<BidListDTO>())).ReturnsAsync(newBidList);

            // Act
            var result = await _controller.Create(newBidList);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<BidListDTO>(createdAtActionResult.Value);
            Assert.Equal(1, returnValue.BidListId);
        }

        /// <summary>
        /// Tests if Create returns BadRequestResult when the model state is invalid.
        /// </summary>
        [Fact]
        public async Task Create_ReturnsBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Account", "The Account field is required.");

            // Act
            var result = await _controller.Create(new BidListDTO
            {
                Account = "TestAccount",
                BidType = "TestBidType",
                Benchmark = "TestBenchmark",
                Commentary = "TestCommentary",
                BidSecurity = "TestSecurity",
                BidStatus = "Open",
                Trader = "TestTrader",
                Book = "TestBook",
                CreationName = "TestCreator",
                RevisionName = "TestRevisor",
                DealName = "TestDeal",
                DealType = "TestDealType",
                SourceListId = "SL1",
                Side = "Buy"
            });


            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
