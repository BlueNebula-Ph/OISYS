namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="CityFilterRequest"/> class represents basic City filter for displaying data.
    /// </summary>
    public class CityFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets the province id of the city.
        /// </summary>
        public int ProvinceId { get; set; }
    }
}
