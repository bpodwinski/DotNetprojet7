using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;
        private readonly LocalDbContext _dbContext;

        public TradeService(
            ITradeRepository tradeRepository,
            LocalDbContext dbContext
        )
        {
            _tradeRepository = tradeRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new trade based on the provided data transfer object (DTO).
        /// </summary>
        /// <param name="dto">The TradeDTO containing the details for the new trade.</param>
        /// <returns>The created TradeDTO, or throws an exception if creation fails.</returns>
        public async Task<TradeDTO?> Create(TradeDTO dto)
        {
            try
            {
                var trade = ToTradeModel(dto);

                await _tradeRepository.Create(trade);
                return ToTradeDTO(trade);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating a new trade.", ex);
            }
        }

        /// <summary>
        /// Deletes a specific trade by its ID.
        /// </summary>
        /// <param name="id">The ID of the trade to delete.</param>
        /// <returns>The deleted TradeDTO, or null if not found.</returns>
        public async Task<TradeDTO?> DeleteById(int id)
        {
            try
            {
                var existingTrade = await _tradeRepository.GetById(id);
                if (existingTrade == null)
                {
                    return null;
                }

                _dbContext.Entry(existingTrade).State = EntityState.Detached;

                var trade = await _tradeRepository.DeleteById(id);
                return trade != null ? ToTradeDTO(trade) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the trade with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a trade by its ID.
        /// </summary>
        /// <param name="id">The ID of the trade to retrieve.</param>
        /// <returns>The TradeDTO if found, or null otherwise.</returns>
        public async Task<TradeDTO?> GetById(int id)
        {
            try
            {
                var trade = await _tradeRepository.GetById(id);
                return trade != null ? ToTradeDTO(trade) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the trade with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all trades as a list of TradeDTOs.
        /// </summary>
        /// <returns>A list of TradeDTOs, or throws an exception if retrieval fails.</returns>
        public async Task<List<TradeDTO>> GetAll()
        {
            try
            {
                var trades = await _tradeRepository.GetAll();
                return trades.Select(ToTradeDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all trades.", ex);
            }
        }

        /// <summary>
        /// Updates an existing trade based on the provided ID and data transfer object (DTO).
        /// </summary>
        /// <param name="id">The ID of the trade to update.</param>
        /// <param name="dto">The TradeDTO containing updated trade details.</param>
        /// <returns>The updated TradeDTO, or null if the trade was not found.</returns>
        public async Task<TradeDTO?> Update(int id, TradeDTO dto)
        {
            try
            {
                var trade = ToTradeModel(dto);
                trade.TradeId = id; // Ensuring ID consistency for update

                var updatedTrade = await _tradeRepository.UpdateAsync(trade);
                return updatedTrade != null ? ToTradeDTO(updatedTrade) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the trade with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Converts a TradeDTO to a Trade entity.
        /// </summary>
        /// <param name="dto">The TradeDTO containing data for the conversion.</param>
        /// <returns>The corresponding Trade entity.</returns>
        private static Trade ToTradeModel(TradeDTO dto) => new()
        {
            TradeId = dto.TradeId,
            Account = dto.Account,
            AccountType = dto.AccountType,
            BuyQuantity = dto.BuyQuantity,
            SellQuantity = dto.SellQuantity,
            BuyPrice = dto.BuyPrice,
            SellPrice = dto.SellPrice,
            TradeDate = dto.TradeDate,
            TradeSecurity = dto.TradeSecurity,
            TradeStatus = dto.TradeStatus,
            Trader = dto.Trader,
            Benchmark = dto.Benchmark,
            Book = dto.Book,
            CreationName = dto.CreationName,
            CreationDate = dto.CreationDate,
            RevisionName = dto.RevisionName,
            RevisionDate = dto.RevisionDate,
            DealName = dto.DealName,
            DealType = dto.DealType,
            SourceListId = dto.SourceListId,
            Side = dto.Side
        };

        /// <summary>
        /// Converts a Trade entity to a TradeDTO.
        /// </summary>
        /// <param name="trade">The Trade entity to convert.</param>
        /// <returns>The corresponding TradeDTO.</returns>
        private TradeDTO ToTradeDTO(Trade trade) => new()
        {
            TradeId = trade.TradeId,
            Account = trade.Account,
            AccountType = trade.AccountType,
            BuyQuantity = trade.BuyQuantity,
            SellQuantity = trade.SellQuantity,
            BuyPrice = trade.BuyPrice,
            SellPrice = trade.SellPrice,
            TradeDate = trade.TradeDate,
            TradeSecurity = trade.TradeSecurity,
            TradeStatus = trade.TradeStatus,
            Trader = trade.Trader,
            Benchmark = trade.Benchmark,
            Book = trade.Book,
            CreationName = trade.CreationName,
            CreationDate = trade.CreationDate,
            RevisionName = trade.RevisionName,
            RevisionDate = trade.RevisionDate,
            DealName = trade.DealName,
            DealType = trade.DealType,
            SourceListId = trade.SourceListId,
            Side = trade.Side
        };
    }
}
