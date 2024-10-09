namespace P7CreateRestApi.Domain
{
    public class Trade
    {
        public int TradeId { get; set; }
        public required string Account { get; set; }
        public required string AccountType { get; set; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }
        public required string TradeSecurity { get; set; }
        public required string TradeStatus { get; set; }
        public required string Trader { get; set; }
        public required string Benchmark { get; set; }
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
