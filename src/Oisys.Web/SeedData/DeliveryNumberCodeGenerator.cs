﻿namespace Oisys.Web.SeedData
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.ValueGeneration;

    /// <summary>
    /// This is used for auto generating codes.
    /// Should not be used when migrated to sql server.
    /// </summary>
    public class DeliveryNumberCodeGenerator : ValueGenerator<int>
    {
        private const int InitialCode = 100001;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryNumberCodeGenerator"/> class.
        /// </summary>
        public DeliveryNumberCodeGenerator()
        {
        }

        /// <inheritdoc />
        public override bool GeneratesTemporaryValues => false;

        /// <inheritdoc/>
        public override int Next(EntityEntry entry)
        {
            return ((OisysDbContext)entry.Context).Deliveries.LastOrDefault()?.DeliveryNumber + 1 ?? InitialCode;
        }
    }
}