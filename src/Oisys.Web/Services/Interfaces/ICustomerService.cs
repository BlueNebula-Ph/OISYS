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
        /// <param name="customerId">customer id</param>
        /// <param name="customerTransactionId">transaction to modify</param>
        void DeleteCustomerTransaction(int customerId, int customerTransactionId);

        /// <summary>
        /// Modify customer transaction when returning an item
        /// </summary>
        /// <param name="transaction">transaction to modify</param>
        /// <param name="transactionType">Adjustment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Remarks</param>
        void ModifyCustomerTransaction(CustomerTransaction transaction, TransactionType transactionType, decimal? totalAmount, string remarks);

        /// <summary>
        /// Method to add customer transaction using CustomerService
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="transactionType">Adjusment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Credit Memo remarks</param>
        /// <returns>Customer Transaction</returns>
        CustomerTransaction AddCustomerTransaction(int customerId, TransactionType transactionType, decimal? totalAmount, string remarks);
    }
}
