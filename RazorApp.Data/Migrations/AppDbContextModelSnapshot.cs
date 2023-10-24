﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorApp.Data.Database;

#nullable disable

namespace RazorApp.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RazorApp.Data.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Web")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Qty")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<double>("Qty")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Production", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Qty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Productions");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Purchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Purchases");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.PurchaseDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaseId")
                        .HasColumnType("int");

                    b.Property<double>("Qty")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("PurchaseId");

                    b.ToTable("PurchaseDetails");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Contact")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postal")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Web")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Material", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Order", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .IsRequired();

                    b.HasOne("RazorApp.Data.Entities.Supplier", null)
                        .WithMany("Orders")
                        .HasForeignKey("SupplierId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.OrderDetail", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired();

                    b.HasOne("RazorApp.Data.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Product", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.ProductDetail", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Material", "Material")
                        .WithMany("ProductDetails")
                        .HasForeignKey("MaterialId")
                        .IsRequired();

                    b.HasOne("RazorApp.Data.Entities.Product", "Product")
                        .WithMany("ProductDetails")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Production", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Product", "Product")
                        .WithMany("Productions")
                        .HasForeignKey("ProductId")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Purchase", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Customer", null)
                        .WithMany("Purchases")
                        .HasForeignKey("CustomerId");

                    b.HasOne("RazorApp.Data.Entities.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .IsRequired();

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.PurchaseDetail", b =>
                {
                    b.HasOne("RazorApp.Data.Entities.Material", "Material")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("MaterialId")
                        .IsRequired();

                    b.HasOne("RazorApp.Data.Entities.Purchase", "Purchase")
                        .WithMany("PurchaseDetails")
                        .HasForeignKey("PurchaseId")
                        .IsRequired();

                    b.Navigation("Material");

                    b.Navigation("Purchase");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Customer", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Material", b =>
                {
                    b.Navigation("ProductDetails");

                    b.Navigation("PurchaseDetails");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("ProductDetails");

                    b.Navigation("Productions");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Purchase", b =>
                {
                    b.Navigation("PurchaseDetails");
                });

            modelBuilder.Entity("RazorApp.Data.Entities.Supplier", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
