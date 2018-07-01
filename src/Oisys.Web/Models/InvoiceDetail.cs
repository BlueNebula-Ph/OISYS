namespace Oisys.Web.Models
{
    /// <summary>
    /// <see cref="InvoiceDetail"/> for recording invoice details
    /// </summary>
    public class InvoiceDetail : ModelBase
    {
        /// <summary>
        /// Gets or sets property invoice id.
        /// </summary>
        public int InvoiceId { get; set; }

        /// <summary>
        /// Gets or sets navigation property.
        /// </summary>
        public Invoice Invoice { get; set; }
    }
}
