namespace Oisys.Service.Helpers
{
    /// <summary>
    /// Values to define different object states
    /// This is used for mapping to entity framework states
    /// </summary>
    public enum ObjectState
    {
        /// <summary>
        /// The unchanged state
        /// </summary>
        Unchanged,

        /// <summary>
        /// The added state
        /// </summary>
        Added,

        /// <summary>
        /// The modified state
        /// </summary>
        Modified,

        /// <summary>
        /// The deleted state
        /// </summary>
        Deleted,
    }
}
