namespace Oisys.Web.Services
{
    using System;
    using System.Linq;
    using Oisys.Web.Helpers;
    using Oisys.Web.Models;
    using Oisys.Web.Services.Interfaces;

    /// <summary>
    /// Class for customer transactions
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly OisysDbContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerService"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        public CustomerService(OisysDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Delete customer transaction
        /// </summary>
        /// <param name="transaction">trnsaction to delete</param>
        public void DeleteCustomerTransaction(CustomerTransaction transaction)
        {
            transaction.IsDeleted = true;
        }

        /// <summary>
        /// Modify customer transaction when returning an item
        /// </summary>
        /// <param name="customerTransactionId">transaction to modify</param>
        /// <param name="adjustmentType">Adjustment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Remarks</param>
        public void ModifyCustomerTransaction(int customerTransactionId, AdjustmentType adjustmentType, decimal? totalAmount, string remarks)
        {
            var transaction = this.context.CustomerTransactions.SingleOrDefault(c => c.Id == customerTransactionId);

            var credit = adjustmentType == AdjustmentType.Add ? totalAmount : null;
            var debit = adjustmentType == AdjustmentType.Deduct ? totalAmount : null;

            if (transaction != null)
            {
                transaction.Credit = credit;
                transaction.Debit = debit;
                transaction.Description = remarks;
            }
        }

        /// <summary>
        /// Method to add customer transaction using CustomerService
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="creditMemo">Credit Memo created</param>
        /// <param name="adjustmentType">Adjusment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Credit Memo remarks</param>
        public void AddCustomerTransaction(int customerId, CreditMemo creditMemo, AdjustmentType adjustmentType, decimal? totalAmount, string remarks)
        {
            var credit = adjustmentType == AdjustmentType.Add ? totalAmount : null;
            var debit = adjustmentType == AdjustmentType.Deduct ? totalAmount : null;

            var customerTransaction = new CustomerTransaction()
                {
                    CustomerId = customerId,
                    Date = DateTime.Now.ToUniversalTime(),
                    Credit = credit,
                    Debit = debit,
                    TransactionType = remarks,
                    Description = remarks,
                };

            this.context.CustomerTransactions.AddAsync(customerTransaction);

            creditMemo.CustomerTransaction = customerTransaction;
        }
    }
}
