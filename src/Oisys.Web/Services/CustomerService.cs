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
        /// <param name="transaction">transaction to modify</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Remarks</param>
        public void ModifyCustomerTransaction(CustomerTransaction transaction, TransactionType transactionType, decimal? totalAmount, string remarks)
        {
            var credit = transactionType == TransactionType.Credit ? totalAmount : 0m;
            var debit = transactionType == TransactionType.Debit ? totalAmount : 0m;

            // update transaction
            if (transaction != null)
            {
                transaction.Credit = credit;
                transaction.Debit = debit;
                transaction.Description = remarks;
            }

            this.UpdateCustomerBalance(transaction.CustomerId, transactionType, totalAmount);
        }

        /// <summary>
        /// Method to add customer transaction using CustomerService
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="transactionType">Transaction type</param>
        /// <param name="totalAmount">Total amount</param>
        /// <param name="remarks">Credit Memo remarks</param>
        /// <returns>Customer Transaction</returns>
        public CustomerTransaction AddCustomerTransaction(int customerId, TransactionType transactionType, decimal? totalAmount, string remarks)
        {
            var credit = transactionType == TransactionType.Credit ? totalAmount : 0m;
            var debit = transactionType == TransactionType.Debit ? totalAmount : 0m;

            // Add Customer transaction
            var customerTransaction = new CustomerTransaction()
                {
                    CustomerId = customerId,
                    Date = DateTime.Now.ToUniversalTime(),
                    Credit = credit,
                    Debit = debit,
                    TransactionType = remarks,
                    Description = remarks,
                };

            this.context.Add(customerTransaction);

            this.UpdateCustomerBalance(customerId, transactionType, totalAmount);

            return customerTransaction;
        }

        private void UpdateCustomerBalance(int customerId, TransactionType transactionType, decimal? totalAmount)
        {
            // Update Customer balance
            var customer = this.context.Customers
                                .SingleOrDefault(c => c.Id == customerId);

            if (customer != null)
            {
                customer.Balance = transactionType == TransactionType.Debit ?
                    customer.Balance + totalAmount.Value :
                    customer.Balance - totalAmount.Value;
            }
        }
    }
}
