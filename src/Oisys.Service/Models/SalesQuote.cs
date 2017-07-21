namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SalesQuote : ModelBase
    {
        [Required]
        public string Code { get; set; }

        public int CustomerId { get; set; }

        public DateTime Date { get; set; }

        public decimal DeliveryFee { get; set; }

        public ICollection<SalesQuoteDetail> Details { get; set; }

        public Customer Customer { get; set; }
    }
}
