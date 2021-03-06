﻿namespace Oisys.Web.DTO
{
    using System.Collections.Generic;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// Lookup class for provinces.
    /// </summary>
    public class ProvinceLookup : DTOBase
    {
        /// <summary>
        /// Gets or sets the province name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the list of cities under the province.
        /// </summary>
        public IEnumerable<CitySummary> Cities { get; set; }
    }
}
