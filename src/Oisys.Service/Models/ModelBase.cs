namespace Oisys.Service.Models
{
    using System;

    public class ModelBase
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public int LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
