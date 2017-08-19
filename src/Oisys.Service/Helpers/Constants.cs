namespace Oisys.Service.Helpers
{
    /// <summary>
    /// <see cref="Constants"/> class to hold commonly used values object.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Sets the sort direction ascending value
        /// </summary>
        public const string SortDirectionAscending = "asc";

        /// <summary>
        /// Sets the sort direction descending value
        /// </summary>
        public const string SortDirectionDescending = "desc";

        /// <summary>
        /// Sets the default sort direction
        /// </summary>
        public const string DefaultSortDirection = SortDirectionAscending;

        /// <summary>
        /// Sets the default page size
        /// </summary>
        public const int DefaultPageSize = 20;

        /// <summary>
        /// Sets the default page index
        /// </summary>
        public const int DefaultPageIndex = 1;

        /// <summary>
        /// <see cref="TransactionType"/> class to hold commonly used values object.
        /// </summary>
        public static class TransactionType
        {
            /// <summary>
            /// Sets the customer transaction type value
            /// </summary>
            public const string Payment = "Payment";

            /// <summary>
            /// Sets the customer transaction type value
            /// </summary>
            public const string Delivery = "Delivery";
        }
    }
}