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
    }
}
