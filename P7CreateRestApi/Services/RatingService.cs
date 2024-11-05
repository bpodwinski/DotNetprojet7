using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;
using P7CreateRestApi.Domain;
using P7CreateRestApi.DTOs;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly LocalDbContext _dbContext;

        public RatingService(
            IRatingRepository ratingRepository,
            LocalDbContext dbContext
        )
        {
            _ratingRepository = ratingRepository;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Creates a new Rating based on the provided dto.
        /// </summary>
        /// <param name="dto">The RatingDTO containing the data to create the Rating</param>
        /// <returns>The created RatingDTO</returns>
        public async Task<RatingDTO?> Create(RatingDTO dto)
        {
            try
            {
                var rating = ToRating(dto);
                await _ratingRepository.Create(rating);
                return ToRatingDTO(rating);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating a new Rating.", ex);
            }
        }

        /// <summary>
        /// Deletes a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to delete</param>
        /// <returns>The deleted RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> Delete(int id)
        {
            try
            {
                var existingRating = await _ratingRepository.GetById(id);
                if (existingRating == null)
                {
                    return null;
                }

                _dbContext.Entry(existingRating).State = EntityState.Detached;
                var deletedRating = await _ratingRepository.DeleteById(id);
                return deletedRating != null ? ToRatingDTO(deletedRating) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the Rating with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific Rating by ID.
        /// </summary>
        /// <param name="id">The ID of the Rating to retrieve</param>
        /// <returns>The RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> GetById(int id)
        {
            try
            {
                var rating = await _ratingRepository.GetById(id);
                return rating != null ? ToRatingDTO(rating) : null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the Rating with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Retrieves all Rating entities and maps them to DTOs.
        /// </summary>
        /// <returns>A list of RatingDTOs</returns>
        public async Task<List<RatingDTO>> GetAll()
        {
            try
            {
                var ratings = await _ratingRepository.GetAll();
                return ratings.Select(ToRatingDTO).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving all Ratings.", ex);
            }
        }

        /// <summary>
        /// Updates a specific Rating entity.
        /// </summary>
        /// <param name="id">The ID of the Rating to update</param>
        /// <param name="dto">The RatingDTO containing the updated values</param>
        /// <returns>The updated RatingDTO, or null if not found</returns>
        public async Task<RatingDTO?> Update(int id, RatingDTO dto)
        {
            try
            {
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
                return ToRatingDTO(updatedRating);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the Rating with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Converts a RatingDTO to a Rating entity.
        /// </summary>
        /// <param name="dto">The RatingDTO containing data.</param>
        /// <returns>The corresponding Rating entity.</returns>
        private static Rating ToRating(RatingDTO dto) => new()
        {
            Id = dto.Id,
            MoodysRating = dto.MoodysRating,
            SandPRating = dto.SandPRating,
            FitchRating = dto.FitchRating,
            OrderNumber = dto.OrderNumber
        };

        /// <summary>
        /// Converts a Rating entity to a RatingDTO.
        /// </summary>
        /// <param name="rating">The Rating entity to convert</param>
        /// <returns>The corresponding RatingDTO</returns>
        private RatingDTO ToRatingDTO(Rating rating) =>
            new()
            {
                Id = rating.Id,
                MoodysRating = rating.MoodysRating,
                SandPRating = rating.SandPRating,
                FitchRating = rating.FitchRating,
                OrderNumber = rating.OrderNumber
            };
    }
}
