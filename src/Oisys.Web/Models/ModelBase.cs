namespace Oisys.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// <see cref="ModelBase"/> class Create/Update Customer object.
    /// </summary>
    public class ModelBase
    {
        /// <summary>
        /// Gets or sets property Id.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets property Created by.
        /// </summary>
        public int CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets property Created Date.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets property Last Updated By.
        /// </summary>
        public int LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets property Last Updated Date.
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LastUpdatedDate { get; set; }
    }
}
