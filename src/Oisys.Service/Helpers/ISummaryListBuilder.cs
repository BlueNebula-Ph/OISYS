namespace Oisys.Service.Helpers
{
    using System.Linq;
    using System.Threading.Tasks;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// Interface for summary list builder
    /// </summary>
    /// <typeparam name="T">Entity type of summary list to build</typeparam>
    /// <typeparam name="T1">Result type of summary list</typeparam>
    public interface ISummaryListBuilder<T, T1>
    {
        /// <summary>
        /// Builds the summary list
        /// </summary>
        /// <param name="source">The source queryable</param>
        /// <param name="filter">filter object</param>
        /// <returns>The summary list result</returns>
        Task<SummaryList<T1>> BuildAsync(IQueryable<T> source, FilterRequestBase filter);
    }
}
