namespace Oisys.Web.Services.Interfaces
{
    /// <summary>
    /// Defines the structure for AdjustmentService
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Method to compute customer's running balance
        /// </summary>
        /// <param name="customerId">Customer Id</param>
        /// <param name="credit">Credit amount</param>
        /// <param name="debit">Debit amount</param>
        /// <returns>Customer's running balance</returns>
        decimal ComputeRunningBalance(long customerId, decimal? credit, decimal? debit);
    }
}
