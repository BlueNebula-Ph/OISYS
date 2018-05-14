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

            this.UpdateCustomerBalance(customerId);
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

            this.UpdateCustomerBalance(transaction.CustomerId);
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
                    TransactionType = Enum.GetName(typeof(TransactionType), transactionType),
                    Description = remarks,
                };

            this.context.CustomerTransactions.Add(customerTransaction);

            this.UpdateCustomerBalance(customerId);

            return customerTransaction;
        }

        private void UpdateCustomerBalance(int customerId)
        {
            // Update Customer balance
            var customer = this.context.Customers.SingleOrDefault(a => a.Id == customerId);
            var customerTransactions = this.context.CustomerTransactions.Local
                .Where(a => a.CustomerId == customerId && !a.IsDeleted);

            if (customer != null)
            {
                var totalDebit = customerTransactions.Sum(a => a.Debit) ?? 0;
                var totalCredit = customerTransactions.Sum(a => a.Credit) ?? 0;

                customer.Balance = totalDebit - totalCredit;
            }
        }
    }
}
