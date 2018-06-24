namespace Oisys.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ValueGeneration;
    using Microsoft.Extensions.DependencyInjection;
    using Oisys.Web.Models;
    using Microsoft.AspNetCore.Identity;
    using Oisys.Web.SeedData;

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
        /// Gets or sets the adjustments db set.
        /// </summary>
        public DbSet<Adjustment> Adjustments { get; set; }

        /// <summary>
        /// Gets or sets the cash voucher db set.
        /// </summary>
        public DbSet<CashVoucher> CashVouchers { get; set; }

        /// <summary>
        /// Gets or sets the categories db set.
        /// </summary>
        public DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the cities db set.
        /// </summary>
        public DbSet<City> Cities { get; set; }

        /// <summary>
        /// Gets or sets the credit memos db set.
        /// </summary>
        public DbSet<CreditMemo> CreditMemos { get; set; }

        /// <summary>
        /// Gets or sets the credit memo details db set.
        /// </summary>
        public DbSet<CreditMemoDetail> CreditMemoDetails { get; set; }

        /// <summary>
        /// Gets or sets the customers db set.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }

        /// <summary>
        /// Gets or sets the customer transactions db set.
        /// </summary>
        public DbSet<CustomerTransaction> CustomerTransactions { get; set; }

        /// <summary>
        /// Gets or sets the deliveries db set.
        /// </summary>
        public DbSet<Delivery> Deliveries { get; set; }

        /// <summary>
        /// Gets or sets the delivery details db set.
        /// </summary>
        public DbSet<DeliveryDetail> DeliveryDetails { get; set; }

        /// <summary>
        /// Gets or sets the items db set.
        /// </summary>
        public DbSet<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the orders db set.
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the order details db set.
        /// </summary>
        public DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the provinces db set.
        /// </summary>
        public DbSet<Province> Provinces { get; set; }

        /// <summary>
        /// Gets or sets the sales quotes db set.
        /// </summary>
        public DbSet<SalesQuote> SalesQuotes { get; set; }

        /// <summary>
        /// Gets or sets the sales quote details db set.
        /// </summary>
        public DbSet<SalesQuoteDetail> SalesQuoteDetails { get; set; }

        /// <summary>
        /// Gets or sets property Users <see cref="User"/> class.
        /// </summary>
        public DbSet<ApplicationUser> Users { get; set; }

        /// <summary>
        /// Gets or sets the reference types db set.
        /// </summary>
        public DbSet<ReferenceType> ReferenceTypes { get; set; }

        /// <summary>
        /// Gets or sets the references db set.
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

                SeedReferenceTypes(context);
                SeedReferences(context);

                SeedCategories(context);
                SeedProvincesAndCities(context);
                SeedCustomer(context);
                SeedItems(context);
                SeedUsers(context);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Seeds the required values.
        /// </summary>
        /// <param name="app">The application builder</param>
        public static void SeedRequired(IApplicationBuilder app)
        {
            using (var context = app.ApplicationServices.GetRequiredService<OisysDbContext>())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

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

            // Item Adjustments
            modelBuilder.Entity<Adjustment>()
                .HasOne<Item>(d => d.Item)
                .WithMany(p => p.Adjustments)
                .HasForeignKey(p => p.ItemId);

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

            // Delivery Details
            modelBuilder.Entity<DeliveryDetail>()
                .HasOne<Delivery>(d => d.Delivery)
                .WithMany(p => p.Details)
                .HasForeignKey(p => p.DeliveryId);

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

            modelBuilder.HasSequence<int>("CreditMemoCode")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<CreditMemo>()
                .Property(o => o.Code)
                .HasDefaultValueSql("NEXT VALUE FOR CreditMemoCode");

            // TODO: Remove when migrated to sql server
            modelBuilder.Entity<CreditMemo>()
                .Property(o => o.Code)
                .HasValueGenerator(typeof(CreditMemoCodeGenerator));

            modelBuilder.HasSequence<int>("OrderCode")
               .StartsAt(100000)
               .IncrementsBy(1);

            modelBuilder.Entity<Order>()
                .Property(o => o.Code)
                .HasDefaultValueSql("NEXT VALUE FOR OrderCode");

            // TODO: Remove when migrated to sql server
            modelBuilder.Entity<Order>()
                .Property(o => o.Code)
                .HasValueGenerator(typeof(OrderCodeGenerator));

            modelBuilder.HasSequence<int>("VoucherNumber")
               .StartsAt(100000)
               .IncrementsBy(1);

            modelBuilder.Entity<CashVoucher>()
                .Property(o => o.VoucherNumber)
                .HasDefaultValueSql("NEXT VALUE FOR VoucherNumber");

            // TODO: Remove when migrated to sql server
            modelBuilder.Entity<CashVoucher>()
                .Property(o => o.VoucherNumber)
                .HasValueGenerator(typeof(VoucherNumberGenerator));

            modelBuilder.HasSequence<int>("QuotationCode")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<SalesQuote>()
                .Property(o => o.QuoteNumber)
                .HasDefaultValueSql("NEXT VALUE FOR QuotationCode");

            // TODO: Remove when migrated to sql server
            modelBuilder.Entity<SalesQuote>()
                .Property(o => o.QuoteNumber)
                .HasValueGenerator(typeof(SalesQuotationNumberGenerator));

            modelBuilder.HasSequence<int>("DeliveryNumber")
                .StartsAt(100000)
                .IncrementsBy(1);

            modelBuilder.Entity<Delivery>()
                .Property(o => o.DeliveryNumber)
                .HasDefaultValueSql("NEXT VALUE FOR DeliveryNumber");

            // TODO: Remove when migrated to sql server
            modelBuilder.Entity<Delivery>()
                .Property(o => o.DeliveryNumber)
                .HasValueGenerator(typeof(DeliveryNumberCodeGenerator));
        }

        private static void SeedCustomer(OisysDbContext context)
        {
            if (!context.Customers.Any())
            {
                context.Customers.Add(new Customer
                {
                    Name = "Mickey Mouse",
                    Email = "mickey@disney.com",
                    ContactNumber = "399-39-39",
                    ContactPerson = "Mr. Mickey",
                    Address = "Disneyland Tokyo",
                    CityId = 4,
                    ProvinceId = 1,
                    Terms = "term1",
                    Discount = 10m,
                    PriceListId = 1,
                });

                context.Customers.Add(new Customer
                {
                    Name = "Mario Cart",
                    Email = "mario@nintendo.com",
                    ContactNumber = "383-33-00",
                    ContactPerson = "Mr. Luigi",
                    Address = "Japan Nintendo Park",
                    CityId = 5,
                    ProvinceId = 2,
                    Terms = "term2",
                    Discount = 5m,
                    PriceListId = 2,
                });
            }
        }

        private static void SeedReferenceTypes(OisysDbContext context)
        {
            if (!context.ReferenceTypes.Any())
            {
                var referenceTypes = new List<ReferenceType>
                {
                    new ReferenceType { Code = "Price List" },
                    new ReferenceType { Code = "Voucher Category" },
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
                    new Reference { ReferenceTypeId = 1, Code = "Main Price" },
                    new Reference { ReferenceTypeId = 1, Code = "Walk-In Price" },
                    new Reference { ReferenceTypeId = 1, Code = "N.E. Price" },
                    new Reference { ReferenceTypeId = 2, Code = "Automotive" },
                    new Reference { ReferenceTypeId = 2, Code = "Electrical" },
                    new Reference { ReferenceTypeId = 2, Code = "Hardware" },
                    new Reference { ReferenceTypeId = 2, Code = "Industrial" },
                    new Reference { ReferenceTypeId = 2, Code = "Other" },
                };
                context.References.AddRange(references);
            }
        }

        private static void SeedProvincesAndCities(OisysDbContext context)
        {
            if (!context.Provinces.Any())
            {
                var provinces = new List<Province>
                {
                    new Province
                    {
                        Name = "NCR",
                        Cities = new List<City>
                        {
                            new City { Name = "Manila" },
                            new City { Name = "Makati" },
                            new City { Name = "Las Pinas" },
                            new City { Name = "Paranaque" },
                        },
                    },
                    new Province
                    {
                        Name = "Cavite",
                        Cities = new List<City>
                        {
                            new City { Name = "Tanza" },
                            new City { Name = "Indang" },
                            new City { Name = "Gen Trias" },
                        },
                    },
                    new Province
                    {
                        Name = "Bulacan",
                        Cities = new List<City>
                        {
                            new City { Name = "Malolos" },
                            new City { Name = "Bocaue" },
                            new City { Name = "Obando" },
                        },
                    },
                };
                context.Provinces.AddRange(provinces);
            }
        }

        private static void SeedCategories(OisysDbContext context)
        {
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Material" },
                    new Category { Name = "Lead" },
                    new Category { Name = "Paint" },
                    new Category { Name = "Tubes" },
                };
                context.Categories.AddRange(categories);
            }
        }

        private static void SeedItems(OisysDbContext context)
        {
            if (!context.Items.Any())
            {
                var items = new List<Item>
                {
                    new Item { Code = "0001", Name = "Item Number 1", CategoryId = 1, Description = "Item 1. This is item 1", CurrentQuantity = 100, ActualQuantity = 100, Unit = "pcs.", MainPrice = 1919.99m, NEPrice = 2929.99m, WalkInPrice = 3939.39m },
                    new Item { Code = "0002", Name = "Item Number 2", CategoryId = 2, Description = "Item 2. This is item 2", CurrentQuantity = 200, ActualQuantity = 200, Unit = "stacks", MainPrice = 919.99m, NEPrice = 929.99m, WalkInPrice = 939.39m },
                    new Item { Code = "0003", Name = "Item Number 3", CategoryId = 1, Description = "Item 3. This is item 3", CurrentQuantity = 300, ActualQuantity = 300, Unit = "makes", MainPrice = 111m, NEPrice = 222m, WalkInPrice = 333m },
                    new Item { Code = "0004", Name = "Item Number 4", CategoryId = 3, Description = "Item 4. This is item 4", CurrentQuantity = 400, ActualQuantity = 400, Unit = "pc", MainPrice = 12.50m, NEPrice = 29.50m, WalkInPrice = 39.50m },
                    new Item { Code = "0005", Name = "Item Number 5", CategoryId = 4, Description = "Item 5. This is item 5", CurrentQuantity = 500, ActualQuantity = 500, Unit = "shards", MainPrice = 400m, NEPrice = 500m, WalkInPrice = 600m },
                    new Item { Code = "0006", Name = "Item Number 6", CategoryId = 4, Description = "Item 6. This is item 6", CurrentQuantity = 600, ActualQuantity = 600, Unit = "rolls", MainPrice = 1211m, NEPrice = 1222m, WalkInPrice = 1233m },
                };
                context.Items.AddRange(items);
            }
        }

        private static void SeedUsers(OisysDbContext context)
        {
            if (!context.Users.Any())
            {
                var newUser = new ApplicationUser { Username = "Admin", Firstname = "Admin", Lastname = "User", AccessRights = "admin,canView,canWrite,canDelete" };
                var password = new PasswordHasher<ApplicationUser>().HashPassword(newUser, "Admin");
                newUser.PasswordHash = password;

                context.Users.Add(newUser);
            }
        }
    }
}
