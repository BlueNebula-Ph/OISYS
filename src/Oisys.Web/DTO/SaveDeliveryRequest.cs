namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="SaveDeliveryRequest"/> class Create/Update Delivery object.
    /// </summary>
    public class SaveDeliveryRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property Plate Number.
        /// </summary>
        public string PlateNumber { get; set; }

        /// <summary>
        /// Gets or sets property Province Id.
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// Gets or sets property City Id.
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public IEnumerable<SaveDeliveryDetailRequest> Details { get; set; }
    }
}
