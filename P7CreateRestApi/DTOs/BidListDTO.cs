using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a BidList entity.
    /// </summary>
    public class BidListDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the BidList.
        /// </summary>
        public int BidListId { get; set; }

        /// <summary>
        /// Gets or sets the account associated with the bid.
        /// </summary>
        [Required(ErrorMessage = "The Account field is required.")]
        public required string Account { get; set; }

        /// <summary>
        /// Gets or sets the type of bid.
        /// </summary>
        [Required(ErrorMessage = "The BidType field is required.")]
        public required string BidType { get; set; }

        /// <summary>
        /// Gets or sets the bid quantity.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "BidQuantity must be greater than or equal to 0.")]
        public double? BidQuantity { get; set; }

        /// <summary>
        /// Gets or sets the ask quantity.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "AskQuantity must be greater than or equal to 0.")]
        public double? AskQuantity { get; set; }

        /// <summary>
        /// Gets or sets the bid price.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Bid must be greater than or equal to 0.")]
        public double? Bid { get; set; }

        /// <summary>
        /// Gets or sets the ask price.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Ask must be greater than or equal to 0.")]
        public double? Ask { get; set; }

        /// <summary>
        /// Gets or sets the benchmark associated with the bid.
        /// </summary>
        [Required(ErrorMessage = "The Benchmark field is required.")]
        public required string Benchmark { get; set; }

        /// <summary>
        /// Gets or sets the date of the bid list.
        /// </summary>
        public DateTime? BidListDate { get; set; }

        /// <summary>
        /// Gets or sets the commentary related to the bid.
        /// </summary>
        [Required(ErrorMessage = "The Commentary field is required.")]
        [StringLength(255, ErrorMessage = "The Commentary field cannot exceed 255 characters.")]
        public required string Commentary { get; set; }

        /// <summary>
        /// Gets or sets the security associated with the bid.
        /// </summary>
        [Required(ErrorMessage = "The BidSecurity field is required.")]
        public required string BidSecurity { get; set; }

        /// <summary>
        /// Gets or sets the status of the bid.
        /// </summary>
        [Required(ErrorMessage = "The BidStatus field is required.")]
        public required string BidStatus { get; set; }

        /// <summary>
        /// Gets or sets the trader responsible for the bid.
        /// </summary>
        [Required(ErrorMessage = "The Trader field is required.")]
        public required string Trader { get; set; }

        /// <summary>
        /// Gets or sets the book related to the bid.
        /// </summary>
        [Required(ErrorMessage = "The Book field is required.")]
        public required string Book { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who created the bid list.
        /// </summary>
        [Required(ErrorMessage = "The CreationName field is required.")]
        public required string CreationName { get; set; }

        /// <summary>
        /// Gets or sets the date when the bid list was created.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who last revised the bid list.
        /// </summary>
        [Required(ErrorMessage = "The RevisionName field is required.")]
        public required string RevisionName { get; set; }

        /// <summary>
        /// Gets or sets the date when the bid list was last revised.
        /// </summary>
        public DateTime? RevisionDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the deal associated with the bid list.
        /// </summary>
        [Required(ErrorMessage = "The DealName field is required.")]
        public required string DealName { get; set; }

        /// <summary>
        /// Gets or sets the type of deal associated with the bid list.
        /// </summary>
        [Required(ErrorMessage = "The DealType field is required.")]
        public required string DealType { get; set; }

        /// <summary>
        /// Gets or sets the source list identifier.
        /// </summary>
        [Required(ErrorMessage = "The SourceListId field is required.")]
        public required string SourceListId { get; set; }

        /// <summary>
        /// Gets or sets the side of the trade (buy or sell).
        /// </summary>
        [Required(ErrorMessage = "The Side field is required.")]
        public required string Side { get; set; }
    }
}
