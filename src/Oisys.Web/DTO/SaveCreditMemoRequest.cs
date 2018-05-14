namespace Oisys.Web.DTO
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;
    using Newtonsoft.Json;

    /// <summary>
    /// <see cref="SaveCreditMemoRequest"/> class Create/Update CreditMemo object.
    /// </summary>
    public class SaveCreditMemoRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public int Code { get; set; }

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
        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerId { get; set; }

        /// <summary>
        /// Gets or sets property Details.
        /// </summary>
        public ICollection<SaveCreditMemoDetailRequest> Details { get; set; }
    }
}