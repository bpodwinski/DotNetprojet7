using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        /// <summary>
        /// Asynchronously creates a new Trade based on the provided model.
        /// </summary>
        public async Task<TradeDTO?> CreateAsync(TradeModel model)
        {
            var trade = new Trade
            {
                Account = model.Account,
                AccountType = model.AccountType,
                BuyQuantity = model.BuyQuantity,
                SellQuantity = model.SellQuantity,
                BuyPrice = model.BuyPrice,
                SellPrice = model.SellPrice,
                TradeDate = model.TradeDate,
                TradeSecurity = model.TradeSecurity,
                TradeStatus = model.TradeStatus,
                Trader = model.Trader,
                Benchmark = model.Benchmark,
                Book = model.Book,
                CreationName = model.CreationName,
                CreationDate = DateTime.Now,
                RevisionName = model.RevisionName,
                RevisionDate = model.RevisionDate,
                DealName = model.DealName,
                DealType = model.DealType,
                SourceListId = model.SourceListId,
                Side = model.Side
            };
            await _tradeRepository.Create(trade);
            return ToDTO(trade);
        }

        /// <summary>
        /// Asynchronously deletes a Trade by its ID.
        /// </summary>
        public async Task<TradeDTO?> DeleteByIdAsync(int id)
        {
            var trade = await _tradeRepository.DeleteById(id);
            return trade is not null ? ToDTO(trade) : null;
        }

        /// <summary>
        /// Asynchronously retrieves a Trade by its ID.
        /// </summary>
        public async Task<TradeDTO?> GetByIdAsync(int id)
        {
            var trade = await _tradeRepository.GetById(id);
            return trade is not null ? ToDTO(trade) : null;
        }

        /// <summary>
        /// Asynchronously retrieves all Trade entities.
        /// </summary>
        public async Task<List<TradeDTO>> ListAsync()
        {
            var trades = await _tradeRepository.GetAll();
            return trades.Select(ToDTO).ToList();
        }

        /// <summary>
        /// Asynchronously updates an existing Trade by its ID.
        /// </summary>
        public async Task<TradeDTO?> UpdateByIdAsync(int id, TradeModel model)
        {
            var trade = new Trade
            {
                TradeId = id,
                Account = model.Account,
                AccountType = model.AccountType,
                BuyQuantity = model.BuyQuantity,
                SellQuantity = model.SellQuantity,
                BuyPrice = model.BuyPrice,
                SellPrice = model.SellPrice,
                TradeDate = model.TradeDate,
                TradeSecurity = model.TradeSecurity,
                TradeStatus = model.TradeStatus,
                Trader = model.Trader,
                Benchmark = model.Benchmark,
                Book = model.Book,
                CreationName = model.CreationName,
                RevisionName = model.RevisionName,
                RevisionDate = model.RevisionDate,
                DealName = model.DealName,
                DealType = model.DealType,
                SourceListId = model.SourceListId,
                Side = model.Side
            };

            var updatedTrade = await _tradeRepository.UpdateAsync(trade);
            return updatedTrade is not null ? ToDTO(updatedTrade) : null;
        }

        /// <summary>
        /// Converts a Trade entity to a TradeDTO.
        /// </summary>
        private TradeDTO ToDTO(Trade trade) => new TradeDTO
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
