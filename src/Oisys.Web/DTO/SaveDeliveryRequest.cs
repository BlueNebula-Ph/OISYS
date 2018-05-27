namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using BlueNebula.Common.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="SaveDeliveryRequest"/> class Create/Update Delivery object.
    /// </summary>
    public class SaveDeliveryRequest : DTOBase, IObjectWithState
    {
        /// <summary>
        /// Gets or sets property Date.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets property Driver.
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public IEnumerable<SaveDeliveryDetailRequest> Details { get; set; }

        /// <summary>
        /// Gets or sets the state property
        /// </summary>
        [JsonIgnore]
        public ObjectState State { get; set; }
    }
}
