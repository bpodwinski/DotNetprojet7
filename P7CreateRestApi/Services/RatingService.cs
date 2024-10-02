using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Models;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;

        public RatingService(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        /// <summary>
        /// Creates a new Rating based on the provided model.
        /// </summary>
        /// <param name="model">The RatingModel containing the data to create the Rating</param>
        /// <returns>The created RatingDTO</returns>
        public async Task<RatingDTO?> CreateAsync(RatingModel model)
        {
            var rating = new Rating
            {
                MoodysRating = model.MoodysRating,
                SandPRating = model.SandPRating,
                FitchRating = model.FitchRating,
                OrderNumber = model.OrderNumber
            };
            await _ratingRepository.CreateAsync(rating);
            return ToDTO(rating);
        }

        /// <summary>
        /// Deletes a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to delete</param>
        /// <returns>The deleted RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> DeleteByIdAsync(int id)
        {
            // Vérifier si le Rating existe avant suppression
            var existingRating = await _ratingRepository.GetByIdAsync(id);
            if (existingRating == null)
            {
                return null;
            }

            var deletedRating = await _ratingRepository.DeleteByIdAsync(id);
            return deletedRating != null ? ToDTO(deletedRating) : null;
        }

        /// <summary>
        /// Retrieves a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to retrieve</param>
        /// <returns>The RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> GetByIdAsync(int id)
        {
            var rating = await _ratingRepository.GetByIdAsync(id);
            return rating != null ? ToDTO(rating) : null;
        }

        /// <summary>
        /// Retrieves all Rating entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of RatingDTOs</returns>
        public async Task<List<RatingDTO>> ListAsync()
        {
            var ratings = await _ratingRepository.ListAsync();
            return ratings.Select(ToDTO).ToList();
        }

        /// <summary>
        /// Updates a specific Rating entity.
        /// </summary>
        /// <param name="id">The ID of the Rating to update</param>
        /// <param name="model">The RatingModel containing the updated values</param>
        /// <returns>The updated RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> UpdateByIdAsync(int id, RatingModel model)
        {
            // Vérification si le Rating existe avant mise à jour
            var existingRating = await _ratingRepository.GetByIdAsync(id);
            if (existingRating == null)
            {
                return null;
            }

            existingRating.MoodysRating = model.MoodysRating;
            existingRating.SandPRating = model.SandPRating;
            existingRating.FitchRating = model.FitchRating;
            existingRating.OrderNumber = model.OrderNumber;

            var updatedRating = await _ratingRepository.UpdateAsync(existingRating);
            return ToDTO(updatedRating);
        }

        /// <summary>
        /// Converts a Rating entity to a RatingDTO.
        /// </summary>
        /// <param name="rating">The Rating entity to convert</param>
        /// <returns>The corresponding RatingDTO</returns>
        private RatingDTO ToDTO(Rating rating) =>
            new RatingDTO
            {
                Id = rating.Id,
                MoodysRating = rating.MoodysRating,
                SandPRating = rating.SandPRating,
                FitchRating = rating.FitchRating,
                OrderNumber = rating.OrderNumber
            };
    }
}
