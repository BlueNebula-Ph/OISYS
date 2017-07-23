namespace Oisys.Service
{
    using Microsoft.EntityFrameworkCore;
    using Oisys.Service.Models;

    /// <summary>
    /// <see cref="OisysDbContext"/> class DbContext.
    /// </summary>
    public class OisysDbContext : DbContext
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
        }
    }
}
