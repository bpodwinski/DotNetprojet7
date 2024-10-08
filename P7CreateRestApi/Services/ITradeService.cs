using P7CreateRestApi.DTOs;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for Trade service.
    /// </summary>
    public interface ITradeService
    {
        /// <summary>
        /// Asynchronously creates a new Trade.
        /// </summary>
        /// <param name="dto">The TradeDTO containing the details of the trade to create.</param>
        /// <returns>The created TradeDTO, or null if the creation failed.</returns>
        Task<TradeDTO?> Create(TradeDTO dto);

        /// <summary>
        /// Asynchronously deletes a Trade by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade to delete.</param>
        /// <returns>The deleted TradeDTO, or null if the Trade was not found.</returns>
        Task<TradeDTO?> Delete(int id);

        /// <summary>
        /// Asynchronously retrieves a Trade by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade to retrieve.</param>
        /// <returns>The TradeDTO, or null if the Trade was not found.</returns>
        Task<TradeDTO?> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves a list of all Trades.
        /// </summary>
        /// <returns>A list of TradeDTOs.</returns>
        Task<List<TradeDTO>> GetAll();

        /// <summary>
        /// Asynchronously updates an existing Trade by its ID.
        /// </summary>
        /// <param name="id">The ID of the Trade to update.</param>
        /// <param name="dto">The TradeDTO containing the updated details of the trade.</param>
        /// <returns>The updated TradeDTO, or null if the Trade was not found.</returns>
        Task<TradeDTO?> Update(int id, TradeDTO dto);
    }
}
