using P7CreateRestApi.DTOs;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for BidList service to manage CRUD operations on BidList entities.
    /// </summary>
    public interface IBidListService
    {
        /// <summary>
        /// Asynchronously creates a new BidList entity based on the provided DTO.
        /// </summary>
        /// <param name="dto">The DTO containing data for the new BidList.</param>
        /// <returns>A task representing the asynchronous operation, with the created BidListDTO.</returns>
        Task<BidListDTO?> Create(BidListDTO dto);

        /// <summary>
        /// Asynchronously deletes a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList to delete.</param>
        /// <returns>A task representing the asynchronous operation, with the deleted BidListDTO, or null if not found.</returns>
        Task<BidListDTO?> DeleteById(int id);

        /// <summary>
        /// Asynchronously retrieves a BidList entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the BidList to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, with the BidListDTO, or null if not found.</returns>
        Task<BidListDTO?> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves all BidList entities.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a list of BidListDTOs.</returns>
        Task<List<BidListDTO>> GetAll();

        /// <summary>
        /// Asynchronously updates a BidList entity with the provided data.
        /// </summary>
        /// <param name="id">The ID of the BidList to update.</param>
        /// <param name="dto">The DTO containing the updated values.</param>
        /// <returns>A task representing the asynchronous operation, with the updated BidListDTO, or null if not found.</returns>
        Task<BidListDTO?> UpdateById(int id, BidListDTO dto);
    }
}
