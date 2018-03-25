namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using BlueNebula.Common.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="SaveCreditMemoRequest"/> class Create/Update CreditMemo object.
    /// </summary>
    public class SaveCreditMemoRequest : DTOBase, IObjectWithState
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
        public string Driver { get; set; }

        /// <summary>
        /// Gets or sets property CustomerId.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<SaveCreditMemoDetailRequest> Details { get; set; }

        /// <summary>
        /// Gets or sets the state property
        /// </summary>
        [JsonIgnore]
        public ObjectState State { get; set; }
    }
}
