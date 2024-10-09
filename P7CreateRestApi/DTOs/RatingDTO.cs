using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Rating entity.
    /// </summary>
    public class RatingDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Rating.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Moody's rating for the entity.
        /// </summary>
        [Required(ErrorMessage = "The MoodysRating field is required.")]
        public string MoodysRating { get; set; }

        /// <summary>
        /// Gets or sets Standard and Poor's rating for the entity.
        /// </summary>
        [Required(ErrorMessage = "The SandPRating field is required.")]
        public string SandPRating { get; set; }

        /// <summary>
        /// Gets or sets Fitch's rating for the entity.
        /// </summary>
        [Required(ErrorMessage = "The FitchRating field is required.")]
        public string FitchRating { get; set; }

        /// <summary>
        /// Gets or sets the order number of the rating.
        /// </summary>
        [Required(ErrorMessage = "The OrderNumber field is required.")]
        [Range(0, byte.MaxValue, ErrorMessage = "The OrderNumber must be a positive number between 0 and 255.")]
        public byte? OrderNumber { get; set; }
    }
}
