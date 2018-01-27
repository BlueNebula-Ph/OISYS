namespace Oisys.Web.DTO
{
    using System.ComponentModel.DataAnnotations;
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="SaveCityRequest"/> class Create/Update SaveCityRequest object.
    /// </summary>
    public class SaveCityRequest : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property ProvinceId.
        /// </summary>
        public int? ProvinceId { get; set; }
    }
}
