using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oisys.Service.Models
{
    public class SalesQuote : ModelBase
    {
        public int QuoteNumber { get; set; }
        public int CustomerId { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string Item { get; set; }
        public int CategoryId { get; set; }
        public string Descrption { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal DeliveryFee { get; set; }
        public decimal Total { get; set; }
    }
}
