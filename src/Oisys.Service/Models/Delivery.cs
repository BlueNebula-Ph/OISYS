namespace Oisys.Service.Models
{
    using System;

    public class Delivery : ModelBase
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string Driver { get; set; }
    }
}
