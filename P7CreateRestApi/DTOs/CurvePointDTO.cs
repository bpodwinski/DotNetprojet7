using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.DTOs
{
    /// <summary>
    /// Data Transfer Object for a curve point.
    /// </summary>
    public class CurvePointDTO
    {
        /// <summary>
        /// Gets or sets the unique ID of the curve point.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the ID of the associated curve.
        /// Must be a positive integer between 0 and 255.
        /// </summary>
        [Range(0, byte.MaxValue, ErrorMessage = "Le CurveId doit être un entier positif compris entre 0 et 255.")]
        public byte? CurveId { get; set; }

        /// <summary>
        /// Gets or sets the date when the curve point is effective.
        /// </summary>
        public DateTime? AsOfDate { get; set; }

        /// <summary>
        /// Gets or sets the term (duration) of the curve point.
        /// Must be greater than or equal to 0.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "Le Term doit être supérieur ou égal à 0.")]
        public double? Term { get; set; }

        /// <summary>
        /// Gets or sets the value of the curve point.
        /// Must be greater than or equal to 0.
        /// </summary>
        [Range(0, double.MaxValue, ErrorMessage = "La valeur CurvePointValue doit être supérieure ou égale à 0.")]
        public double? CurvePointValue { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the curve point.
        /// </summary>
        public DateTime? CreationDate { get; set; }
    }
}
