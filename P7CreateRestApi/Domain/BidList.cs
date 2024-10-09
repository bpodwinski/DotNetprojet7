namespace P7CreateRestApi.Domain
{
    public class BidList
    {
        public int BidListId { get; set; }
        public required string Account { get; set; }
        public required string BidType { get; set; }
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public required string Benchmark { get; set; }
        public DateTime? BidListDate { get; set; }
        public required string Commentary { get; set; }
        public required string BidSecurity { get; set; }
        public required string BidStatus { get; set; }
        public required string Trader { get; set; }
        public required string Book { get; set; }
        public required string CreationName { get; set; }
        public DateTime? CreationDate { get; set; }
        public required string RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }
        public required string DealName { get; set; }
        public required string DealType { get; set; }
        public required string SourceListId { get; set; }
        public required string Side { get; set; }
    }
}
