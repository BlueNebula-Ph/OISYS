namespace Oisys.Service.Services
{
    using System.Linq;
    using Oisys.Service.Helpers;
    using Oisys.Service.Services.Interfaces;
    using Oisys.Service.Models;

    /// <summary>
    /// Class for inventory adjustments
    /// </summary>
    internal class AdjustmentService : IAdjustmentService
    {
        private readonly OisysDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdjustmentService"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        public AdjustmentService(OisysDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Method to adjust item current quantity when transaction is an order transaction
        /// </summary>
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        public void ModifyActualQuantity(Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType)
        {
            if (item != null)
            {
                if (adjustmentType == AdjustmentType.Deduct)
                {
                    item.ActualQuantity = item.ActualQuantity - adjustmentQuantity;
                }
                else
                {
                    item.ActualQuantity = item.ActualQuantity + adjustmentQuantity;
                }
            }
        }

        /// <summary>
        /// Method to adjust item actual quantity when transaction is a delivery transaction
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        public void ModifyCurrentQuantity(OisysDbContext context, Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType)
        {
            if (item != null)
            {
                if (adjustmentType == AdjustmentType.Deduct)
                {
                    item.CurrentQuantity = item.CurrentQuantity - adjustmentQuantity;
                }
                else
                {
                    item.CurrentQuantity = item.CurrentQuantity + adjustmentQuantity;
                }

                context.Update(item);
            }
        }
    }
}
