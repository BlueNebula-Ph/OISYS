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
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        void ModifyCurrentQuantity(Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType);

        /// <summary>
        /// Method to adjust item actual quantity when transaction is a delivery transaction
        /// </summary>
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        void ModifyActualQuantity(Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType);
    }
}
