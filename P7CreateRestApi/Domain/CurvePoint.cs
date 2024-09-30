namespace Dot.Net.WebApi.Domain
{
    public class CurvePoint
    {
        public byte? CurveId { get; set; }
        public DateTime? AsOfDate { get; set; }
        public double? Term { get; set; }
        public double? CurvePointValue { get; set; }
    }
}