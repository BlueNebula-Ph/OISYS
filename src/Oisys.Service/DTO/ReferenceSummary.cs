namespace Oisys.Service.DTO
{
    /// <summary>
    /// <see cref="ReferenceSummary"/> class represents common values that would be used throughout the application.
    /// </summary>
    public class ReferenceSummary : DTOBase
    {
        /// <summary>
        /// Gets or sets property Code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets property ReferenceTypeId.
        /// </summary>
        public int ReferenceTypeId { get; set; }

        /// <summary>
        /// Gets or sets property ParentId.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets ReferenceType Code.
        /// </summary>
        public string ReferenceTypeCode { get; set; }

        /// <summary>
        /// Gets or sets Parent Code.
        /// </summary>
        public string ParentCode { get; set; }
    }
}
