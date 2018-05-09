namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="CreditMemoDetailSummary"/> class represents the child of CreditSummary object.
    /// </summary>
    public class CreditMemoDetailSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Item.
        /// </summary>
        public string Item { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets property Price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets property total price.
        /// </summary>
        public decimal TotalPrice
        {
            get { return this.Quantity * this.Price; }
        }
    }
}
