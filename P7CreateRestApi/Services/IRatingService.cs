using P7CreateRestApi.DTOs;

namespace P7CreateRestApi.Services
{
    /// <summary>
    /// Interface for managing operations on Rating entities.
    /// </summary>
    public interface IRatingService
    {
        /// <summary>
        /// Asynchronously creates a new Rating entity.
        /// </summary>
        /// <param name="dto">The RatingDTO containing the details of the rating to create.</param>
        /// <returns>The created RatingDTO, or null if the creation failed.</returns>
        Task<RatingDTO?> Create(RatingDTO dto);

        /// <summary>
        /// Asynchronously deletes a Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the rating to delete.</param>
        /// <returns>The deleted RatingDTO, or null if the rating was not found.</returns>
        Task<RatingDTO?> Delete(int id);

        /// <summary>
        /// Asynchronously retrieves a Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the rating to retrieve.</param>
        /// <returns>The RatingDTO, or null if the rating was not found.</returns>
        Task<RatingDTO?> GetById(int id);

        /// <summary>
        /// Asynchronously retrieves all Rating entities.
        /// </summary>
        /// <returns>A list of RatingDTOs.</returns>
        Task<List<RatingDTO>> GetAll();

        /// <summary>
        /// Asynchronously updates an existing Rating entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the rating to update.</param>
        /// <param name="dto">The RatingDTO containing the updated details of the rating.</param>
        /// <returns>The updated RatingDTO, or null if the rating was not found.</returns>
        Task<RatingDTO?> Update(int id, RatingDTO dto);
    }
}
