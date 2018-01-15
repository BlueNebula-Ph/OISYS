namespace Oisys.Web.Helpers
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// Helpers for entityframework change tracking
    /// </summary>
    public static class ChangeTrackingHelpers
    {
        /// <summary>
        /// Converts the state of the tracked entity into it's EF state counterpart
        /// </summary>
        /// <param name="node">The entity node</param>
        public static void ConvertStateOfNode(EntityEntryGraphNode node)
        {
            IObjectWithState entity = (IObjectWithState)node.Entry.Entity;
            node.Entry.State = ConvertToEFState(entity.State);
        }

        private static EntityState ConvertToEFState(ObjectState objectState)
        {
            EntityState efState = EntityState.Unchanged;
            switch (objectState)
            {
                case ObjectState.Added:
                    efState = EntityState.Added;
                    break;
                case ObjectState.Modified:
                    efState = EntityState.Modified;
                    break;
                case ObjectState.Deleted:
                    efState = EntityState.Deleted;
                    break;
                case ObjectState.Unchanged:
                    efState = EntityState.Unchanged;
                    break;
            }

            return efState;
        }
    }
}
