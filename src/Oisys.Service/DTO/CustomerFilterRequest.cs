namespace Oisys.Service.DTO
{
    /// <summary>
    /// <see cref="FilterRequestBase"/> class represents basic Customer filter for displaying data.
    /// </summary>
    public class CustomerFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets property Terms.
        /// </summary>
        public string Terms { get; set; }

        /// <summary>
        /// Gets or sets property Discount.
        /// </summary>
        public string Discount { get; set; }

        /// <summary>
        /// Gets or sets property CityId.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets property ProvinceId.
        /// </summary>
        public int? ProvinceId { get; set; }
    }
}
