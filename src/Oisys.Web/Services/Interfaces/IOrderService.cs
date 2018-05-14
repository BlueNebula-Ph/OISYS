namespace Oisys.Web.Services.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// Order service for managing orders
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Updates the returned quantity of an order detail
        /// </summary>
        /// <param name="creditMemoId">The credit memo id</param>
        /// <param name="orderDetailId">The order detail id</param>
        /// <param name="quantityReturned">The quantity returned</param>
        /// <returns>Task</returns>
        Task UpdateQuantityReturnedForOrderDetail(int creditMemoId, int orderDetailId, decimal quantityReturned);
    }
}
