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

        /// <summary>
        /// Common remarks for adjustments
        /// </summary>
        public static class AdjustmentRemarks
        {
            /// <summary>
            /// The initial quantity text.
            /// </summary>
            public const string InitialQuantity = "Initial Quantity";

            /// <summary>
            /// The order created text.
            /// </summary>
            public const string OrderCreated = "Order Created";

            /// <summary>
            /// The order updated text.
            /// </summary>
            public const string OrderUpdated = "Order Updated";

            /// <summary>
            /// The order deleted text.
            /// </summary>
            public const string OrderDeleted = "Order Deleted";

            /// <summary>
            /// The delivery created text.
            /// </summary>
            public const string DeliveryCreated = "Delivery Created";

            /// <summary>
            /// The delivery updated text.
            /// </summary>
            public const string DeliveryUpdated = "Delivery Updated";

            /// <summary>
            /// The delivery deleted text.
            /// </summary>
            public const string DeliveryDeleted = "Delivery Deleted";
        }
    }
}