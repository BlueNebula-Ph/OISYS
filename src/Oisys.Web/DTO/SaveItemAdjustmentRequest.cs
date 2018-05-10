namespace Oisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using Oisys.Web.Helpers;

    /// <summary>
    /// <see cref="SaveItemAdjustmentRequest"/> class Adjust Item object.
    /// </summary>
    public class SaveItemAdjustmentRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property AdjustmentType.
        /// </summary>
        [Required]
        public AdjustmentType AdjustmentType { get; set; }

        /// <summary>
        /// Gets or sets property AdjustmentQuantity.
        /// </summary>
        [Required]
        public decimal AdjustmentQuantity { get; set; }

        /// <summary>
        /// Gets or sets property Remarks.
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// Gets or sets property Operator.
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets property Machine.
        /// </summary>
        public string Machine { get; set; }
    }
}
