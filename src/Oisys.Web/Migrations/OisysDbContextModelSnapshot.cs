using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Oisys.Web;

namespace Oisys.Web.Migrations
{
    [DbContext(typeof(OisysDbContext))]
    partial class OisysDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("Relational:Sequence:.CreditMemoCode", "'CreditMemoCode', '', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.OrderCode", "'OrderCode', '', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("Relational:Sequence:.VoucherNumber", "'VoucherNumber', '', '100000', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Oisys.Web.Models.Adjustment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AdjustmentDate");

                    b.Property<string>("AdjustmentType");

                    b.Property<int>("ItemId");

                    b.Property<string>("Machine");

                    b.Property<string>("Operator");

                    b.Property<decimal>("Quantity");

                    b.Property<string>("QuantityType");

                    b.Property<string>("Remarks");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Adjustments");
                });

            modelBuilder.Entity("Oisys.Web.Models.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessRights");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Firstname");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Lastname");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Oisys.Web.Models.CashVoucher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount");

                    b.Property<string>("Category");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("PayTo");

                    b.Property<string>("ReleasedBy");

                    b.Property<int>("VoucherNumber")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR VoucherNumber");

                    b.HasKey("Id");

                    b.ToTable("CashVouchers");
                });

            modelBuilder.Entity("Oisys.Web.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Oisys.Web.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.Property<int>("ProvinceId");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Oisys.Web.Models.CreditMemo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR CreditMemoCode");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Driver");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CreditMemos");
                });

            modelBuilder.Entity("Oisys.Web.Models.CreditMemoDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CreditMemoId");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<int>("OrderDetailId");

                    b.Property<decimal>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("CreditMemoId");

                    b.HasIndex("OrderDetailId");

                    b.ToTable("CreditMemoDetails");
                });

            modelBuilder.Entity("Oisys.Web.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<decimal>("Balance");

                    b.Property<int>("CityId");

                    b.Property<string>("ContactNumber");

                    b.Property<string>("ContactPerson");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<decimal>("Discount");

                    b.Property<string>("Email");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("PriceListId");

                    b.Property<int>("ProvinceId");

                    b.Property<string>("Terms");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("PriceListId");

                    b.HasIndex("ProvinceId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Oisys.Web.Models.CustomerTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal?>("Credit");

                    b.Property<int>("CreditMemoId");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal?>("Debit");

                    b.Property<string>("Description");

                    b.Property<int>("InvoiceId");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("TransactionType")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("CustomerTransactions");
                });

            modelBuilder.Entity("Oisys.Web.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Driver")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("Oisys.Web.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("ActualQuantity");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<decimal>("CurrentQuantity");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<decimal?>("MainPrice");

                    b.Property<decimal?>("NEPrice");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Thickness");

                    b.Property<string>("Unit");

                    b.Property<decimal?>("WalkInPrice");

                    b.Property<string>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Oisys.Web.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR OrderCode");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("DiscountAmount");

                    b.Property<decimal>("DiscountPercent");

                    b.Property<DateTime?>("DueDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<decimal>("TotalAmount");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Oisys.Web.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("DeliveryId");

                    b.Property<int>("ItemId");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<int>("OrderId");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Quantity");

                    b.Property<decimal>("QuantityReturned");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Oisys.Web.Models.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("Oisys.Web.Models.Reference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<int>("ReferenceTypeId");

                    b.HasKey("Id");

                    b.HasIndex("ReferenceTypeId");

                    b.ToTable("References");
                });

            modelBuilder.Entity("Oisys.Web.Models.ReferenceType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getutcdate()");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ReferenceTypes");
                });

            modelBuilder.Entity("Oisys.Web.Models.SalesQuote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int?>("CustomerId");

                    b.Property<DateTime>("Date");

                    b.Property<decimal>("DeliveryFee");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<string>("QuoteNumber");

                    b.Property<decimal>("TotalAmount");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("SalesQuotes");
                });

            modelBuilder.Entity("Oisys.Web.Models.SalesQuoteDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("ItemId");

                    b.Property<int>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedDate");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Quantity");

                    b.Property<int>("SalesQuoteId");

                    b.Property<decimal>("TotalPrice");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("SalesQuoteId");

                    b.ToTable("SalesQuoteDetails");
                });

            modelBuilder.Entity("Oisys.Web.Models.Adjustment", b =>
                {
                    b.HasOne("Oisys.Web.Models.Item", "Item")
                        .WithMany("Adjustments")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.City", b =>
                {
                    b.HasOne("Oisys.Web.Models.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.CreditMemo", b =>
                {
                    b.HasOne("Oisys.Web.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.CreditMemoDetail", b =>
                {
                    b.HasOne("Oisys.Web.Models.CreditMemo", "CreditMemo")
                        .WithMany("Details")
                        .HasForeignKey("CreditMemoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oisys.Web.Models.OrderDetail", "OrderDetail")
                        .WithMany()
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.Customer", b =>
                {
                    b.HasOne("Oisys.Web.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oisys.Web.Models.Reference", "PriceList")
                        .WithMany()
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oisys.Web.Models.Province", "Province")
                        .WithMany()
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.CustomerTransaction", b =>
                {
                    b.HasOne("Oisys.Web.Models.Customer", "Customer")
                        .WithMany("Transactions")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.Item", b =>
                {
                    b.HasOne("Oisys.Web.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.Order", b =>
                {
                    b.HasOne("Oisys.Web.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.OrderDetail", b =>
                {
                    b.HasOne("Oisys.Web.Models.Delivery", "Delivery")
                        .WithMany("Details")
                        .HasForeignKey("DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oisys.Web.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oisys.Web.Models.Order", "Order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.Reference", b =>
                {
                    b.HasOne("Oisys.Web.Models.ReferenceType", "ReferenceType")
                        .WithMany()
                        .HasForeignKey("ReferenceTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Oisys.Web.Models.SalesQuote", b =>
                {
                    b.HasOne("Oisys.Web.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Oisys.Web.Models.SalesQuoteDetail", b =>
                {
                    b.HasOne("Oisys.Web.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Oisys.Web.Models.SalesQuote", "SalesQuote")
                        .WithMany("Details")
                        .HasForeignKey("SalesQuoteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
