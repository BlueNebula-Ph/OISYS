﻿namespace Oisys.Web.DTO
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using BlueNebula.Common.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="SaveCreditMemoDetailRequest"/> class represents the child of CreditMemo object.
    /// </summary>
    public class SaveCreditMemoDetailRequest : DTOBase, IObjectWithState
    {
        /// <summary>
        /// Gets or sets property CreditMemoId.
        /// </summary>
        [Required]
        public int CreditMemoId { get; set; }

        /// <summary>
        /// Gets or sets property ItemId.
        /// </summary>
        [Required]
        public int ItemId { get; set; }

        /// <summary>
        /// Gets or sets property Quantity.
        /// </summary>
        [Required]
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether gets or sets property whether to put back the returned item to inventory.
        /// </summary>
        public bool ShouldAddBackToInventory { get; set; }

        /// <summary>
        /// Gets or sets a value the orderdetailid
        /// </summary>
        public int OrderDetailId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether order detail is deleted.
        /// </summary>
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the state property
        /// </summary>
        [JsonIgnore]
        public ObjectState State { get; set; }
    }
}
