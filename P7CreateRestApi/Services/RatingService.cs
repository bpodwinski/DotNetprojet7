using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
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
        /// Creates a new Rating based on the provided dto.
        /// </summary>
        /// <param name="dto">The RatingDTO containing the data to create the Rating</param>
        /// <returns>The created RatingDTO</returns>
        public async Task<RatingDTO?> Create(RatingDTO dto)
        {
            var rating = new Rating
            {
                MoodysRating = dto.MoodysRating,
                SandPRating = dto.SandPRating,
                FitchRating = dto.FitchRating,
                OrderNumber = dto.OrderNumber
            };
            await _ratingRepository.Create(rating);
            return ToDTO(rating);
        }

        /// <summary>
        /// Deletes a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to delete</param>
        /// <returns>The deleted RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> Delete(int id)
        {
            var existingRating = await _ratingRepository.GetById(id);
            if (existingRating == null)
            {
                return null;
            }

            var deletedRating = await _ratingRepository.DeleteById(id);
            return deletedRating != null ? ToDTO(deletedRating) : null;
        }

        /// <summary>
        /// Retrieves a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to retrieve</param>
        /// <returns>The RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> GetById(int id)
        {
            var rating = await _ratingRepository.GetById(id);
            return rating != null ? ToDTO(rating) : null;
        }

        /// <summary>
        /// Retrieves all Rating entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of RatingDTOs</returns>
        public async Task<List<RatingDTO>> GetAll()
        {
            var ratings = await _ratingRepository.GetAll();
            return ratings.Select(ToDTO).ToList();
        }

        /// <summary>
        /// Updates a specific Rating entity.
        /// </summary>
        /// <param name="id">The ID of the Rating to update</param>
        /// <param name="dto">The RatingDTO containing the updated values</param>
        /// <returns>The updated RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> Update(int id, RatingDTO dto)
        {
            // Vérification si le Rating existe avant mise à jour
            var existingRating = await _ratingRepository.GetById(id);
            if (existingRating == null)
            {
                return null;
            }

            existingRating.MoodysRating = dto.MoodysRating;
            existingRating.SandPRating = dto.SandPRating;
            existingRating.FitchRating = dto.FitchRating;
            existingRating.OrderNumber = dto.OrderNumber;

            var updatedRating = await _ratingRepository.Update(existingRating);
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
