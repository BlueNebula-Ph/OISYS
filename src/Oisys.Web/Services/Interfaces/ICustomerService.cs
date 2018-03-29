namespace Oisys.Web.Services.Interfaces
{
    using Oisys.Web.Helpers;
    using Oisys.Web.Models;

    /// <summary>
    /// Defines the structure for AdjustmentService
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Delete customer transaction
        /// </summary>
        /// <param name="customerTransactionId">transaction to modify</param>
        void DeleteCustomerTransaction(int customerTransactionId);

        /// <summary>
        /// Modify customer transaction when returning an item
        /// </summary>
        /// <param name="customerTransactionId">transaction to modify</param>
        /// <param name="adjustmentType">Adjustment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Remarks</param>
        void ModifyCustomerTransaction(int customerTransactionId, AdjustmentType adjustmentType, decimal? totalAmount, string remarks);

        /// <summary>
        /// Method to add customer transaction using CustomerService
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="creditMemo">Credit Memo</param>
        /// <param name="adjustmentType">Adjusment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Credit Memo remarks</param>
        void AddCustomerTransaction(int customerId, CreditMemo creditMemo, AdjustmentType adjustmentType, decimal? totalAmount, string remarks);
    }
}
