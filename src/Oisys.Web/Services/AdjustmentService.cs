namespace Oisys.Web.Services
{
    using System;
    using BlueNebula.Common.Helpers;
    using Oisys.Web.Helpers;
    using Oisys.Web.Models;
    using Oisys.Web.Services.Interfaces;

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
                switch (quantityType)
                {
                    case QuantityType.CurrentQuantity:
                        this.AdjustCurrentQuantity(item, adjustmentType, adjustmentQuantity);
                        break;
                    case QuantityType.ActualQuantity:
                        this.AdjustActualQuantity(item, adjustmentType, adjustmentQuantity);
                        break;
                    default:
                        this.AdjustCurrentQuantity(item, adjustmentType, adjustmentQuantity);
                        this.AdjustActualQuantity(item, adjustmentType, adjustmentQuantity);
                        break;
                }

                this.SaveAdjustment(item, adjustmentQuantity, quantityType, adjustmentType, remarks);
            }
        }

        private void AdjustCurrentQuantity(Item item, AdjustmentType adjustmentType, decimal adjustmentQuantity)
        {
            item.CurrentQuantity = adjustmentType == AdjustmentType.Add ?
                            item.CurrentQuantity + adjustmentQuantity :
                            item.CurrentQuantity - adjustmentQuantity;
        }

        private void AdjustActualQuantity(Item item, AdjustmentType adjustmentType, decimal adjustmentQuantity)
        {
            item.ActualQuantity = adjustmentType == AdjustmentType.Add ?
                            item.ActualQuantity + adjustmentQuantity :
                            item.ActualQuantity - adjustmentQuantity;
        }

        private void SaveAdjustment(Item item, decimal adjustmentQuantity, QuantityType quantityType, AdjustmentType adjustmentType, string remarks)
        {
            var adjustment = new Adjustment
            {
                ItemId = item.Id,
                AdjustmentDate = DateTime.Now,
                AdjustmentType = adjustmentType.GetDisplayName(),
                QuantityType = quantityType.GetDisplayName(),
                Quantity = adjustmentQuantity,
                Remarks = remarks,
            };

            this.context.Add(adjustment);
        }
    }
}