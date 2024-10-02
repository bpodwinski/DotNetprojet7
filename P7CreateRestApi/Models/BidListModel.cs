using System.ComponentModel.DataAnnotations;

namespace P7CreateRestApi.Models
{
    public class BidListModel
    {
        [Required(ErrorMessage = "Le champs Account est requis")]
        public string Account { get; set; }
        [Required(ErrorMessage = "Le champs BidType est requis")]
        public string BidType { get; set; }
        public double? BidQuantity { get; set; }
        public double? AskQuantity { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        [Required(ErrorMessage = "Le champs Benchmark est requis")]
        public string Benchmark { get; set; }
        public DateTime? BidListDate { get; set; }
        [Required(ErrorMessage = "Le champs Commentary est requis")]
        public string Commentary { get; set; }
        [Required(ErrorMessage = "Le champs BidSecurity est requis")]
        public string BidSecurity { get; set; }
        [Required(ErrorMessage = "Le champs BidStatus est requis")]
        public string BidStatus { get; set; }
        [Required(ErrorMessage = "Le champs Trader est requis")]
        public string Trader { get; set; }
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
