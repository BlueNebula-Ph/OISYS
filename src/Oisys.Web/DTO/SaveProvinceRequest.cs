﻿namespace Oisys.Web.DTO
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="SaveProvinceRequest"/> class Create/Update SaveProvinceRequest object.
    /// </summary>
    public class SaveProvinceRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets collection of subcategories property.
        /// </summary>
        public IEnumerable<SaveCityRequest> Cities { get; set; }
    }
}
