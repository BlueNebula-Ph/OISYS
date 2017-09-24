namespace Oisys.Service.Services.Interfaces
{
    using Oisys.Service.Helpers;
    using Oisys.Service.Models;

    /// <summary>
    /// Defines the structure for AdjustmentService
    /// </summary>
    public interface IAdjustmentService
    {
        /// <summary>
        /// Method to adjust item current quantity when transaction is an order transaction
        /// </summary>
        /// <param name="quantityType">Quantity Type</param>
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        /// <param name="remarks">Remarks</param>
        void ModifyQuantity(QuantityType quantityType, Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType, string remarks);
    }
}
