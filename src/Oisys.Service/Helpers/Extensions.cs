namespace Oisys.Service.Helpers
{
    /// <summary>
    /// Custom extension methods
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks if an integer is null or zero
        /// </summary>
        /// <param name="number">The number to check</param>
        /// <returns>True if the number is null or zero, false otherwise</returns>
        public static bool IsNullOrZero(this int? number)
        {
            return !number.HasValue || number.Value == 0;
        }
    }
}
