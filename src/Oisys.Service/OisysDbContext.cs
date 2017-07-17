using Microsoft.EntityFrameworkCore;
using Oisys.Service.Models;

namespace Oisys.Service
{
    public class OisysDbContext : DbContext
    {
        public OisysDbContext(DbContextOptions<OisysDbContext> options) : base(options)
        {
        }

        public DbSet<CreditMemo> CreditMemos { get; set; }
        public DbSet<CreditMemoDetail> CreditMemoDetails { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<DeliveryDetail> DeliveryDetails { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }
        public DbSet<SalesQuote> SalesQuotes { get; set; }
        public DbSet<SalesQuoteDetail> SalesQuoteDetails { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
