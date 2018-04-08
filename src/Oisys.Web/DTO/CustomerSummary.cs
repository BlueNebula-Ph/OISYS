namespace Oisys.Web.DTO
{
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// View model for the customer entity.
    /// </summary>
    public class CustomerSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets property Phone Number.
        /// </summary>
        public string ContactNumber { get; set; }

        /// <summary>
        /// Gets or sets property Contact Person.
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// Gets or sets property Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets property City Name.
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Gets or sets property Province Name.
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// Gets or sets property Terms.
        /// </summary>
        public string Terms { get; set; }

        /// <summary>
        /// Gets or sets property Discount.
        /// </summary>
        public string Discount { get; set; }

        /// <summary>
        /// Gets or sets property Price List.
        /// </summary>
        public string PriceList { get; set; }

        /// <summary>
        /// Gets or sets property Tags.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Gets or sets property RunningBalance.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets property Customer transactions.
        /// </summary>
        public IEnumerable<CustomerTransactionSummary> Transactions { get; set; }
    }
}
