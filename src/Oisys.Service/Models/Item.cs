using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oisys.Service.Models
{
    public class Item : ModelBase
    {
        public string Code { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }
}
