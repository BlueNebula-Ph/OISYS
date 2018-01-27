namespace Oisys.Web.DTO
{
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
    }
}
