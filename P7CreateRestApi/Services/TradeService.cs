using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
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
        /// Asynchronously creates a new Trade based on the provided dto.
        /// </summary>
        public async Task<TradeDTO?> Create(TradeDTO dto)
        {
            var trade = new Trade
            {
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
                CreationDate = DateTime.Now,
                RevisionName = dto.RevisionName,
                RevisionDate = dto.RevisionDate,
                DealName = dto.DealName,
                DealType = dto.DealType,
                SourceListId = dto.SourceListId,
                Side = dto.Side
            };
            await _tradeRepository.Create(trade);
            return ToDTO(trade);
        }

        /// <summary>
        /// Asynchronously deletes a Trade by its ID.
        /// </summary>
        public async Task<TradeDTO?> Delete(int id)
        {
            var trade = await _tradeRepository.DeleteById(id);
            return trade is not null ? ToDTO(trade) : null;
        }

        /// <summary>
        /// Asynchronously retrieves a Trade by its ID.
        /// </summary>
        public async Task<TradeDTO?> GetById(int id)
        {
            var trade = await _tradeRepository.GetById(id);
            return trade is not null ? ToDTO(trade) : null;
        }

        /// <summary>
        /// Asynchronously retrieves all Trade entities.
        /// </summary>
        public async Task<List<TradeDTO>> GetAll()
        {
            var trades = await _tradeRepository.GetAll();
            return trades.Select(ToDTO).ToList();
        }

        /// <summary>
        /// Asynchronously updates an existing Trade by its ID.
        /// </summary>
        public async Task<TradeDTO?> Update(int id, TradeDTO dto)
        {
            var trade = new Trade
            {
                TradeId = id,
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
                RevisionName = dto.RevisionName,
                RevisionDate = dto.RevisionDate,
                DealName = dto.DealName,
                DealType = dto.DealType,
                SourceListId = dto.SourceListId,
                Side = dto.Side
            };

            var updatedTrade = await _tradeRepository.UpdateAsync(trade);
            return updatedTrade is not null ? ToDTO(updatedTrade) : null;
        }

        /// <summary>
        /// Converts a Trade entity to a TradeDTO.
        /// </summary>
        private TradeDTO ToDTO(Trade trade) => new()
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
