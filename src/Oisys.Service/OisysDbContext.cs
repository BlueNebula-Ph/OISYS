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
                SeedItems(context);

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
                    Code = "10001",
                    Name = "Mickey Mouse",
                    Email = "mickey@disney.com",
                    ContactNumber = "399-39-39",
                    ContactPerson = "Mr. Mickey",
                    Address = "Disneyland Tokyo",
                    CityId = 4,
                    ProvinceId = 1,
                    Terms = "term1",
                    Discount = "discount1",
                    PriceList = "Main Price",
                    Tag = "Mickey Mouse",
                    Transactions = new List<CustomerTransaction>
                    {
                        new CustomerTransaction { Date = new DateTime(2017, 8, 1), Description = "Order", Debit = 5000m, Credit = 0m, RunningBalance = 5000m },
                        new CustomerTransaction { Date = new DateTime(2017, 8, 4), Description = "Payment", Debit = 0m, Credit = 5000m, RunningBalance = 0m },
                        new CustomerTransaction { Date = new DateTime(2017, 8, 6), Description = "Order", Debit = 4639m, Credit = 0m, RunningBalance = 4639m },
                        new CustomerTransaction { Date = new DateTime(2017, 8, 10), Description = "Payment", Debit = 0m, Credit = 4000m, RunningBalance = 639m },
                        new CustomerTransaction { Date = new DateTime(2017, 8, 18), Description = "Order", Debit = 5000m, Credit = 0m, RunningBalance = 5639m },
                    },
                });

                context.Customers.Add(new Customer
                {
                    Code = "10002",
                    Name = "Mario Cart",
                    Email = "mario@nintendo.com",
                    ContactNumber = "383-33-00",
                    ContactPerson = "Mr. Luigi",
                    Address = "Japan Nintendo Park",
                    CityId = 5,
                    ProvinceId = 2,
                    Terms = "term2",
                    Discount = "discount2",
                    PriceList = "Walk-In Price",
                    Tag = "Mario Cart, Mr. Luigi",
                    Transactions = new List<CustomerTransaction>
                    {
                        new CustomerTransaction { Date = new DateTime(2016, 12, 1), Description = "Order", Debit = 5000m, Credit = 0m, RunningBalance = 5000m },
                        new CustomerTransaction { Date = new DateTime(2016, 12, 22), Description = "Payment", Debit = 0m, Credit = 5000m, RunningBalance = 0m },
                    },
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
                    new Reference { ReferenceTypeId = 2, ParentReferenceId = 1, Code = "Manila" },
                    new Reference { ReferenceTypeId = 2, ParentReferenceId = 2, Code = "Makati" },
                    new Reference { ReferenceTypeId = 2, ParentReferenceId = 1, Code = "Malabon" },
                    new Reference { ReferenceTypeId = 1, Code = "Category 1" },
                    new Reference { ReferenceTypeId = 1, Code = "Cellphones" },
                    new Reference { ReferenceTypeId = 1, Code = "Computers" },
                    new Reference { ReferenceTypeId = 1, Code = "Devices" },
                    new Reference { ReferenceTypeId = 1, Code = "Keyboards" },
                    new Reference { ReferenceTypeId = 1, Code = "Mobile Phones" },
                    new Reference { ReferenceTypeId = 1, Code = "Mouse" },
                    new Reference { ReferenceTypeId = 1, Code = "Category 2" },
                    new Reference { ReferenceTypeId = 1, Code = "Cellphones Case" },
                    new Reference { ReferenceTypeId = 1, Code = "Computers Case" },
                    new Reference { ReferenceTypeId = 1, Code = "Devices Case" },
                    new Reference { ReferenceTypeId = 1, Code = "Keyboards Case" },
                    new Reference { ReferenceTypeId = 1, Code = "Mobile Phones Case" },
                    new Reference { ReferenceTypeId = 1, Code = "Mouse Case" },
                    new Reference { ReferenceTypeId = 1, Code = "Category 3" },
                    new Reference { ReferenceTypeId = 1, Code = "Cellphones Drive" },
                    new Reference { ReferenceTypeId = 1, Code = "Computers Drive" },
                    new Reference { ReferenceTypeId = 1, Code = "Devices Drive" },
                    new Reference { ReferenceTypeId = 1, Code = "Keyboards Drive" },
                    new Reference { ReferenceTypeId = 1, Code = "Mobile Phones Drive" },
                    new Reference { ReferenceTypeId = 1, Code = "Mouse Drive" },
                    new Reference { ReferenceTypeId = 1, Code = "Category 4" },
                    new Reference { ReferenceTypeId = 1, Code = "Cellphones Battery" },
                    new Reference { ReferenceTypeId = 1, Code = "Computers Battery" },
                    new Reference { ReferenceTypeId = 1, Code = "Devices Battery" },
                    new Reference { ReferenceTypeId = 1, Code = "Keyboards Battery" },
                    new Reference { ReferenceTypeId = 1, Code = "Mobile Phones Battery" },
                    new Reference { ReferenceTypeId = 1, Code = "Mouse Battery" },
                };
                context.References.AddRange(references);
            }
        }

        private static void SeedItems(OisysDbContext context)
        {
            if (!context.Items.Any())
            {
                var items = new List<Item>
                {
                    new Item { Code = "Itm10001", Name = "Item Number 1", CategoryId = 8, Description = "Item 1. This is item 1", CurrentQuantity = 100, ActualQuantity = 100, Unit = "pcs.", MainPrice = 1919.99m, NEPrice = 2929.99m, WalkInPrice = 3939.39m },
                    new Item { Code = "Itm10002", Name = "Item Number 2", CategoryId = 9, Description = "Item 2. This is item 2", CurrentQuantity = 100, ActualQuantity = 95, Unit = "stacks", MainPrice = 919.99m, NEPrice = 929.99m, WalkInPrice = 939.39m },
                    new Item { Code = "Itm10003", Name = "Item Number 3", CategoryId = 10, Description = "Item 3. This is item 3", CurrentQuantity = 100, ActualQuantity = 47, Unit = "makes", MainPrice = 111m, NEPrice = 222m, WalkInPrice = 333m },
                    new Item { Code = "Itm10004", Name = "Item Number 4", CategoryId = 11, Description = "Item 4. This is item 4", CurrentQuantity = 100, ActualQuantity = 952, Unit = "pc", MainPrice = 12.50m, NEPrice = 29.50m, WalkInPrice = 39.50m },
                    new Item { Code = "Itm10005", Name = "Item Number 5", CategoryId = 12, Description = "Item 5. This is item 5", CurrentQuantity = 100, ActualQuantity = 20, Unit = "shards", MainPrice = 400m, NEPrice = 500m, WalkInPrice = 600m },
                    new Item { Code = "Itm10006", Name = "Item Number 6", CategoryId = 13, Description = "Item 6. This is item 6", CurrentQuantity = 100, ActualQuantity = 320, Unit = "rolls", MainPrice = 1211m, NEPrice = 1222m, WalkInPrice = 1233m },
                };
                context.Items.AddRange(items);
            }
        }
    }
}
