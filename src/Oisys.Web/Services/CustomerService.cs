namespace Oisys.Web.Services
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
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
        /// <param name="customerId">customer id</param>
        /// <param name="customerTransactionId">trnsaction to delete</param>
        public void DeleteCustomerTransaction(int customerId, int customerTransactionId)
        {
            var transaction = this.context.CustomerTransactions.SingleOrDefault(c => c.Id == customerTransactionId);

            transaction.IsDeleted = true;
        }

        /// <summary>
        /// Modify customer transaction when returning an item
        /// </summary>
        /// <param name="customerId">customer id</param>
        /// <param name="customerTransactionId">transaction to modify</param>
        /// <param name="adjustmentType">Adjustment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Remarks</param>
        public void ModifyCustomerTransaction(int customerId, int customerTransactionId, AdjustmentType adjustmentType, decimal? totalAmount, string remarks)
        {
            var transaction = this.context.CustomerTransactions.SingleOrDefault(c => c.Id == customerTransactionId);

            var credit = adjustmentType == AdjustmentType.Add ? totalAmount : null;
            var debit = adjustmentType == AdjustmentType.Deduct ? totalAmount : null;

            // update transaction
            if (transaction != null)
            {
                transaction.Credit = credit;
                transaction.Debit = debit;
                transaction.Description = remarks;
            }

            this.UpdateCustomerBalance(customerId, adjustmentType, totalAmount);
        }

        /// <summary>
        /// Method to add customer transaction using CustomerService
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="creditMemoId">Credit Memo Id</param>
        /// <param name="invoiceId">Invoice Id</param>
        /// <param name="adjustmentType">Adjusment type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Credit Memo remarks</param>
        public void AddCustomerTransaction(int customerId, int? creditMemoId, int? invoiceId, AdjustmentType adjustmentType, decimal? totalAmount, string remarks)
        {
            var credit = adjustmentType == AdjustmentType.Add ? totalAmount : null;
            var debit = adjustmentType == AdjustmentType.Deduct ? totalAmount : null;

            // Add Customer transaction
            var customerTransaction = new CustomerTransaction()
                {
                    CustomerId = customerId,
                    Date = DateTime.Now.ToUniversalTime(),
                    Credit = credit,
                    Debit = debit,
                    TransactionType = remarks,
                    Description = remarks,
                    CreditMemoId = creditMemoId,
                    InvoiceId = invoiceId,
                };

            this.context.Add(customerTransaction);

            this.UpdateCustomerBalance(customerId, adjustmentType, totalAmount);
        }

        private void UpdateCustomerBalance(int customerId, AdjustmentType adjustmentType, decimal? totalAmount)
        {
            // Update Customer balance
            var customer = this.context.Customers
                                .SingleOrDefault(c => c.Id == customerId);

            if (customer != null)
            {
                customer.Balance = adjustmentType == AdjustmentType.Deduct ? customer.Balance - totalAmount.Value : customer.Balance + totalAmount.Value;
            }
        }
    }
}
