namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;

    public class Delivery : ModelBase
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string Driver { get; set; }
        public ICollection<DeliveryDetail> Details { get; set; }
    }
}
