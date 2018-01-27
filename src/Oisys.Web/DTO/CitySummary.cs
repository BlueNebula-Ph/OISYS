namespace Oisys.Web.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="CitySummary"/> class CitySummary object.
    /// </summary>
    public class CitySummary : DTOBase
    {
        /// <summary>
        /// Gets or sets the name of the city.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the province name of the city.
        /// </summary>
        public string ProvinceName { get; set; }
    }
}
