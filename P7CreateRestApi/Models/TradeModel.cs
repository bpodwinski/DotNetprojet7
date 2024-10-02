using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class TradeModel
    {
        [Required(ErrorMessage = "Le champs Account est requis")]
        public string Account { get; set; }

        [Required(ErrorMessage = "Le champs AccountType est requis")]
        public string AccountType { get; set; }
        public double? BuyQuantity { get; set; }
        public double? SellQuantity { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public DateTime? TradeDate { get; set; }

        [Required(ErrorMessage = "Le champs TradeSecurity est requis")]
        public string TradeSecurity { get; set; }

        [Required(ErrorMessage = "Le champs TradeStatus est requis")]
        public string TradeStatus { get; set; }

        [Required(ErrorMessage = "Le champs Trader est requis")]
        public string Trader { get; set; }

        [Required(ErrorMessage = "Le champs Benchmark est requis")]
        public string Benchmark { get; set; }

        [Required(ErrorMessage = "Le champs Book est requis")]
        public string Book { get; set; }

        [Required(ErrorMessage = "Le champs CreationName est requis")]
        public string CreationName { get; set; }

        [Required(ErrorMessage = "Le champs RevisionName est requis")]
        public string RevisionName { get; set; }
        public DateTime? RevisionDate { get; set; }

        [Required(ErrorMessage = "Le champs DealName est requis")]
        public string DealName { get; set; }

        [Required(ErrorMessage = "Le champs DealType est requis")]
        public string DealType { get; set; }

        [Required(ErrorMessage = "Le champs SourceListId est requis")]
        public string SourceListId { get; set; }

        [Required(ErrorMessage = "Le champs Side est requis")]
        public string Side { get; set; }
    }
}
