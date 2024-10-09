using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a Trade entity.
    /// </summary>
    public class TradeDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Trade.
        /// </summary>
        public int TradeId { get; set; }

        /// <summary>
        /// Gets or sets the account associated with the trade.
        /// </summary>
        [Required(ErrorMessage = "The Account field is required.")]
        public required string Account { get; set; }

        /// <summary>
        /// Gets or sets the type of account involved in the trade.
        /// </summary>
        [Required(ErrorMessage = "The AccountType field is required.")]
        public required string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the quantity bought in the trade.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "BuyQuantity must be greater than or equal to 0.")]
        public double? BuyQuantity { get; set; }

        /// <summary>
        /// Gets or sets the quantity sold in the trade.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "SellQuantity must be greater than or equal to 0.")]
        public double? SellQuantity { get; set; }

        /// <summary>
        /// Gets or sets the price at which the asset was bought.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "BuyPrice must be greater than or equal to 0.")]
        public double? BuyPrice { get; set; }

        /// <summary>
        /// Gets or sets the price at which the asset was sold.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "SellPrice must be greater than or equal to 0.")]
        public double? SellPrice { get; set; }

        /// <summary>
        /// Gets or sets the date of the trade.
        /// </summary>
        public DateTime? TradeDate { get; set; }

        /// <summary>
        /// Gets or sets the security involved in the trade.
        /// </summary>
        [Required(ErrorMessage = "The TradeSecurity field is required.")]
        public required string TradeSecurity { get; set; }

        /// <summary>
        /// Gets or sets the status of the trade.
        /// </summary>
        [Required(ErrorMessage = "The TradeStatus field is required.")]
        public required string TradeStatus { get; set; }

        /// <summary>
        /// Gets or sets the trader responsible for the trade.
        /// </summary>
        [Required(ErrorMessage = "The Trader field is required.")]
        public required string Trader { get; set; }

        /// <summary>
        /// Gets or sets the benchmark for the trade.
        /// </summary>
        [Required(ErrorMessage = "The Benchmark field is required.")]
        public required string Benchmark { get; set; }

        /// <summary>
        /// Gets or sets the book associated with the trade.
        /// </summary>
        [Required(ErrorMessage = "The Book field is required.")]
        public required string Book { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who created the trade.
        /// </summary>
        [Required(ErrorMessage = "The CreationName field is required.")]
        public required string CreationName { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the trade.
        /// </summary>
        public DateTime? CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the person who last revised the trade.
        /// </summary>
        [Required(ErrorMessage = "The RevisionName field is required.")]
        public required string RevisionName { get; set; }

        /// <summary>
        /// Gets or sets the revision date of the trade.
        /// </summary>
        public DateTime? RevisionDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the deal associated with the trade.
        /// </summary>
        [Required(ErrorMessage = "The DealName field is required.")]
        public required string DealName { get; set; }

        /// <summary>
        /// Gets or sets the type of deal associated with the trade.
        /// </summary>
        [Required(ErrorMessage = "The DealType field is required.")]
        public required string DealType { get; set; }

        /// <summary>
        /// Gets or sets the source list identifier for the trade.
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
