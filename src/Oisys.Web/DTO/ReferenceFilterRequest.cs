namespace Oisys.Service.DTO
{
    using BlueNebula.Common.DTOs;

    /// <summary>
    /// <see cref="ReferenceFilterRequest"/> class represents basic Reference filter for displaying data.
    /// </summary>
    public class ReferenceFilterRequest : FilterRequestBase
    {
        /// <summary>
        /// Gets or sets property ParentId.
        /// </summary>
        public int ParentId { get; set; }
    }
}
