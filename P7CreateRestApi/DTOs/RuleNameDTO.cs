using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs
{
    /// <summary>
    /// Data Transfer Object representing a RuleName entity.
    /// </summary>
    public class RuleNameDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the RuleName.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the rule.
        /// </summary>
        [Required(ErrorMessage = "The Name field is required.")]
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the rule.
        /// </summary>
        [Required(ErrorMessage = "The Description field is required.")]
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the JSON representation associated with the rule.
        /// </summary>
        [Required(ErrorMessage = "The Json field is required.")]
        public required string Json { get; set; }

        /// <summary>
        /// Gets or sets the template for the rule.
        /// </summary>
        [Required(ErrorMessage = "The Template field is required.")]
        public required string Template { get; set; }

        /// <summary>
        /// Gets or sets the SQL string associated with the rule.
        /// </summary>
        [Required(ErrorMessage = "The SqlStr field is required.")]
        public required string SqlStr { get; set; }

        /// <summary>
        /// Gets or sets the SQL part associated with the rule.
        /// </summary>
        [Required(ErrorMessage = "The SqlPart field is required.")]
        public required string SqlPart { get; set; }
    }
}
