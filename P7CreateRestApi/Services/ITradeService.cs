using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for Trade service.
    /// </summary>
    public interface ITradeService
    {
        Task<TradeDTO?> CreateAsync(TradeModel model);
        Task<TradeDTO?> DeleteByIdAsync(int id);
        Task<TradeDTO?> GetByIdAsync(int id);
        Task<List<TradeDTO>> ListAsync();
        Task<TradeDTO?> UpdateByIdAsync(int id, TradeModel model);
    }
}
