namespace Oisys.Service.Services
{
    using System;
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
        /// Method to adjust item quantities
        /// </summary>
        /// <param name="quantityType">
        /// The quantity type. Current quantity for order transactions, actual quantity for delivery transactions
        /// </param>
        /// <param name="item"><see cref="Item"/></param>
        /// <param name="adjustmentQuantity">Adjustment Quantity</param>
        /// <param name="adjustmentType">Adjustment Type</param>
        /// <param name="remarks">Remarks</param>
        public void ModifyQuantity(QuantityType quantityType, Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType, string remarks)
        {
            if (item != null)
            {
                switch (adjustmentType)
                {
                    case AdjustmentType.Add:
                        if (quantityType == QuantityType.CurrentQuantity)
                        {
                            item.CurrentQuantity = item.CurrentQuantity + adjustmentQuantity;
                        }
                        else if (quantityType == QuantityType.ActualQuantity)
                        {
                            item.ActualQuantity = item.ActualQuantity + adjustmentQuantity;
                        }

                        break;
                    case AdjustmentType.Deduct:
                        if (quantityType == QuantityType.CurrentQuantity)
                        {
                            item.CurrentQuantity = item.CurrentQuantity - adjustmentQuantity;
                        }
                        else if (quantityType == QuantityType.ActualQuantity)
                        {
                            item.ActualQuantity = item.ActualQuantity - adjustmentQuantity;
                        }

                        break;
                    default:
                        break;
                }

                this.SaveAdjustment(item, adjustmentQuantity, adjustmentType, remarks);
            }
        }

        private void SaveAdjustment(Item item, decimal adjustmentQuantity, AdjustmentType adjustmentType, string remarks)
        {
            var adjustment = new Adjustment
            {
                ItemId = item.Id,
                AdjustmentDate = DateTime.Today,
                AdjustmentType = Enum.GetName(typeof(AdjustmentType), adjustmentType),
                Quantity = adjustmentQuantity,
                Remarks = remarks,
            };

            this.context.Add(adjustment);
        }
    }
}