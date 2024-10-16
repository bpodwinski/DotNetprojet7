using Microsoft.AspNetCore.Mvc;
using Moq;
using P7CreateRestApi.Controllers;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Services;
using Microsoft.Extensions.Logging;

namespace P7CreateRestApi.Test
{
    /// <summary>
    /// Unit test for Update method in BidListController.
    /// </summary>
    public class BidListControllerUpdateTest
    {
        private readonly BidListController _controller;
        private readonly Mock<IBidListService> _mockService;
        private readonly Mock<ILogger<BidListController>> _mockLogger;

        public BidListControllerUpdateTest()
        {
            _mockService = new Mock<IBidListService>();
            _mockLogger = new Mock<ILogger<BidListController>>();
            _controller = new BidListController(_mockService.Object, _mockLogger.Object);
        }

        /// <summary>
        /// Tests if Update returns OkResult when the BidList is successfully updated.
        /// </summary>
        [Fact]
        public async Task Update_ReturnsOkResult()
        {
            // Arrange
            var updatedBidList = new BidListDTO
            {
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
            _mockService.Setup(service => service.Update(1, It.IsAny<BidListDTO>())).ReturnsAsync(updatedBidList);

            // Act
            var result = await _controller.Update(1, updatedBidList);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<BidListDTO>(okResult.Value);
            Assert.Equal(1, returnValue.BidListId);
        }

        /// <summary>
        /// Tests if Update returns NotFoundResult when the BidList is not found.
        /// </summary>
        [Fact]
        public async Task Update_ReturnsNotFoundResult()
        {
            // Arrange
            _mockService.Setup(service => service.Update(1, It.IsAny<BidListDTO>())).ReturnsAsync((BidListDTO)null);

            // Act
            var result = await _controller.Update(1, new BidListDTO
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
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}