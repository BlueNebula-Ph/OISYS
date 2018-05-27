namespace Oisys.Web.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Oisys.Web.Exceptions;
    using Oisys.Web.Services.Interfaces;

    /// <inheritdoc />
    public class OrderService : IOrderService
    {
        private readonly OisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public OrderService(OisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task UpdateQuantityReturnedForOrderDetail(int creditMemoId, int orderDetailId, decimal quantityReturned)
        {
            var previouslyReturnedQuantity = this.dbContext
                .CreditMemoDetails
                .Where(a => a.CreditMemoId != creditMemoId && a.OrderDetailId == orderDetailId)
                .Sum(a => a.Quantity);

            var orderDetail = await this.dbContext.OrderDetails.FindAsync(orderDetailId);
            orderDetail.QuantityReturned = previouslyReturnedQuantity + quantityReturned;

            if (orderDetail.QuantityReturned > orderDetail.Quantity)
            {
                throw new QuantityReturnedException($"Total quantity returned for {orderDetail.Item.Name} cannot be greater than {orderDetail.Quantity}");
            }
        }
    }
}
