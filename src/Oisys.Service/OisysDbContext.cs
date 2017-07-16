using Microsoft.EntityFrameworkCore;
using Oisys.Service.Models;

namespace Oisys.Service
{
    public class OisysDbContext : DbContext
    {
        public OisysDbContext(DbContextOptions<OisysDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
