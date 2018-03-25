namespace Oisys.Web.Services
{
    using System.Linq;
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
        /// Method to compute runinng balance
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="credit">Credit</param>
        /// <param name="debit">Debit</param>
        /// <returns>Balance</returns>
        public decimal ComputeRunningBalance(long customerId, decimal? credit, decimal? debit)
        {
            decimal runningBalance = 0;

            var recentTransaction = this.context.CustomerTransactions.OrderByDescending(c => c.Date).FirstOrDefault(c => c.CustomerId == customerId);

            if (recentTransaction != null)
            {
                runningBalance = recentTransaction.RunningBalance + (credit.HasValue ? credit.Value : 0) - (debit.HasValue ? debit.Value : 0);
            }

            return runningBalance;
        }
    }
}
