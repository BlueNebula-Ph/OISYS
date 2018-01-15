namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using Newtonsoft.Json;
    using Oisys.Web.Helpers;

    /// <summary>
    /// <see cref="SaveDeliveryRequest"/> class Create/Update Delivery object.
    /// </summary>
    public class SaveDeliveryRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property Driver.
        /// </summary>
        [Required]
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public IEnumerable<SaveOrderDetailRequest> Details { get; set; }

        /// <summary>
        /// Gets or sets the state property
        /// </summary>
        [JsonIgnore]
        public ObjectState State { get; set; }
    }
}
