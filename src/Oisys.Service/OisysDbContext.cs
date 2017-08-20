namespace Oisys.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Oisys.Service.Models;

    /// <summary>
    /// <see cref="OisysDbContext"/> class DbContext.
    /// </summary>
    public class OisysDbContext : DbContext, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OisysDbContext"/> class.
        /// </summary>
        /// <param name="options">Context options</param>
        public OisysDbContext(DbContextOptions<OisysDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets property creditMemo <see cref="CreditMemo"/> class.
        /// </summary>
        public DbSet<CreditMemo> CreditMemos { get; set; }

        /// <summary>
        /// Gets or sets property Credit Memo details <see cref="CreditMemoDetail"/> class.
        /// </summary>
        public DbSet<CreditMemoDetail> CreditMemoDetails { get; set; }

        /// <summary>
        /// Gets or sets property Customers <see cref="Customer"/> class.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets property Customers <see cref="CustomerTransaction"/> class.
        /// </summary>
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }

        /// <summary>
        /// Gets or sets property Deliveries <see cref="Delivery"/> class.
        /// </summary>
        public DbSet<Delivery> Deliveries { get; set; }

        /// <summary>
        /// Gets or sets property Items <see cref="Item"/> class.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets property Orders <see cref="Order"/> class.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets property Order Details <see cref="OrderDetail"/> class.
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets property Sales Quotes <see cref="SalesQuote"/> class.
        /// </summary>
        public DbSet<SalesQuote> SalesQuotes { get; set; }

        /// <summary>
        /// Gets or sets property Sales Quote details <see cref="SalesQuoteDetail"/> class.
        /// </summary>
        public DbSet<SalesQuoteDetail> SalesQuoteDetails { get; set; }

        /// <summary>
        /// Gets or sets property Users <see cref="User"/> class.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets property Users <see cref="ReferenceType"/> class.
        /// </summary>
        public DbSet<ReferenceType> ReferenceTypes { get; set; }

        /// <summary>
        /// Gets or sets property Users <see cref="Reference"/> class.
        /// </summary>
        public DbSet<Reference> References { get; set; }

        /// <summary>
        /// Seeds the database with test data.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public static void Seed(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<OisysDbContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                SeedCustomer(context);
                SeedReferenceTypes(context);
                SeedReferences(context);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// This method sets up the foreign keys of entities
        /// </summary>
        /// <param name="modelBuilder">ModelBuilder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Credit Memo Detail
            modelBuilder.Entity<CreditMemoDetail>()
                .HasOne<CreditMemo>(d => d.CreditMemo)
                .WithMany(p => p.Details)
                .HasForeignKey(p => p.CreditMemoId);

            // Customer
            modelBuilder.Entity<CustomerTransaction>()
                .HasOne<Customer>(d => d.Customer)
                .WithMany(p => p.Transactions)
                .HasForeignKey(p => p.CustomerId);

            // Order
            modelBuilder.Entity<OrderDetail>()
                .HasOne<Order>(d => d.Order)
                .WithMany(p => p.Details)
                .HasForeignKey(p => p.OrderId);

            // Sales Quote
            modelBuilder.Entity<SalesQuoteDetail>()
                .HasOne<SalesQuote>(d => d.SalesQuote)
                .WithMany(p => p.Details)
                .HasForeignKey(p => p.SalesQuoteId);

            // TODO: Set default values for Date columns
            // Set default value for CreatedDate
            modelBuilder.Entity<Customer>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<Reference>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql("getutcdate()");

            modelBuilder.Entity<ReferenceType>()
                .Property(e => e.CreatedDate)
                .HasDefaultValueSql("getutcdate()");
        }

        private static void SeedCustomer(OisysDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Customer
                {
                    Code = "A",
                    Name = "A",
                    Email = "a@a.com",
                    ContactNumber = "a",
                    ContactPerson = "a",
                    Address = "A",
                    CityId = 1,
                    ProvinceId = 1,
                    Terms = "term1",
                    Discount = "discount1",
                    PriceList = "A",
                });

                context.Customers.Add(new Customer
                {
                    Code = "B",
                    Name = "B",
                    Email = "b@b.com",
                    ContactNumber = "b",
                    ContactPerson = "b",
                    Address = "B",
                    CityId = 1,
                    ProvinceId = 1,
                    Terms = "term2",
                    Discount = "discount2",
                    PriceList = "B",
                });
            }
        }

        private static void SeedReferenceTypes(OisysDbContext context)
        {
            if (!context.ReferenceTypes.Any())
            {
                var referenceTypes = new List<ReferenceType>
                {
                    new ReferenceType { Code = "Category" },
                    new ReferenceType { Code = "City" },
                    new ReferenceType { Code = "Province" },
                };
                context.ReferenceTypes.AddRange(referenceTypes);
            }
        }

        private static void SeedReferences(OisysDbContext context)
        {
            if (!context.References.Any())
            {
                var references = new List<Reference>
                {
                    new Reference { ReferenceTypeId = 3, Code = "NCR" },
                    new Reference { ReferenceTypeId = 3, Code = "Quezon" },
                    new Reference { ReferenceTypeId = 3, Code = "Cavite" },
                    new Reference { ReferenceTypeId = 2, ParentId = 1, Code = "Manila" },
                    new Reference { ReferenceTypeId = 2, ParentId = 2, Code = "Makati" },
                    new Reference { ReferenceTypeId = 2, ParentId = 1, Code = "Malabon" },
                    new Reference { ReferenceTypeId = 1, Code = "Category 1" },
                };
                context.References.AddRange(references);
            }
        }
    }
}
