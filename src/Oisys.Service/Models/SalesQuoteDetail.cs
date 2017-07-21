namespace Oisys.Service.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SalesQuoteDetail : ModelBase
    {
        [Required]
        public int SalesQuoteId { get; set; }

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public int ItemId { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public SalesQuote SalesQuote { get; set; }
    }
}
