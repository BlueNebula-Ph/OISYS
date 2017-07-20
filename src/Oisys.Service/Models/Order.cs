namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;

    public class Order : ModelBase
    {
        public string Code { get; set; }

        public int CustomerId { get; set; }

        public ICollection<OrderDetail> Details { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }
    }
}
