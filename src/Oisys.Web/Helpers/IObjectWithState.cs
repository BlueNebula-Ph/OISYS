namespace Oisys.Service.Helpers
{
    /// <summary>
    /// Used to keep track of the entity states
    /// </summary>
    public interface IObjectWithState
    {
        /// <summary>
        /// Gets or sets the object state of an entity
        /// </summary>
        ObjectState State { get; set; }
    }
}
