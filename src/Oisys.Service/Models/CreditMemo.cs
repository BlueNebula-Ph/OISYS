namespace Oisys.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CreditMemo : ModelBase
    {
        public string Code { get; set; }
        public DateTime Date { get; set; }
        public string Driver { get; set; }
        public ICollection<CreditMemoDetail> Details { get; set; }
    }
}
