namespace Oisys.Service.Services
{
    using Oisys.Service.Helpers;
    using Oisys.Service.Models;
    using Oisys.Service.Services.Interfaces;

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
                switch (adjustmentType)
                {
                    case AdjustmentType.Add:
                        item.ActualQuantity = item.ActualQuantity + adjustmentQuantity;
                        break;
                    case AdjustmentType.Deduct:
                        item.ActualQuantity = item.ActualQuantity - adjustmentQuantity;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Method to adjust item actual quantity when transaction is a delivery transaction
        /// </summary>
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        public void ModifyCurrentQuantity(Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType)
        {
            if (item != null)
            {
                switch (adjustmentType)
                {
                    case AdjustmentType.Add:
                        item.CurrentQuantity = item.CurrentQuantity + adjustmentQuantity;
                        break;
                    case AdjustmentType.Deduct:
                        item.CurrentQuantity = item.CurrentQuantity - adjustmentQuantity;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
