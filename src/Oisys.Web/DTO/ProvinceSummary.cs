namespace Oisys.Web.DTO
{
    using System.Collections.Generic;
    using System.Linq;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="ProvinceSummary"/> class ProvinceSummary object.
    /// </summary>
    public class ProvinceSummary : DTOBase
    {
        private IEnumerable<CitySummary> cities;

        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets collection of cities.
        /// </summary>
        public IEnumerable<CitySummary> Cities
        {
            get { return this.cities.Where(a => !a.IsDeleted); }
            set { this.cities = value; }
        }
    }
}
