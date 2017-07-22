﻿namespace Oisys.Service
{
    using Microsoft.EntityFrameworkCore;
    using Oisys.Service.Models;

    public class OisysDbContext : DbContext
    {
        public OisysDbContext(DbContextOptions<OisysDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<CreditMemo> CreditMemos { get; set; }

        public DbSet<CreditMemoDetail> CreditMemoDetails { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }

        public DbSet<DeliveryDetail> DeliveryDetails { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<SalesQuote> SalesQuotes { get; set; }

        public DbSet<SalesQuoteDetail> SalesQuoteDetails { get; set; }

        public DbSet<User> Users { get; set; }

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

            // Delivery
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
        }
    }
}
