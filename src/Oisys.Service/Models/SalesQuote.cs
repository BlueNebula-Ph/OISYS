using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oisys.Service.Models
{
    public class SalesQuote : ModelBase
    {
        public string Code { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
        public decimal DeliveryFee { get; set; }
        public ICollection<SalesQuoteDetail> Details { get; set; }
    }
}
