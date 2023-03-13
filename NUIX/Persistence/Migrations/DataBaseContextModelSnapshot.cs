﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Investment", b =>
                {
                    b.Property<int>("InvestmentId")
                        .HasColumnType("int");

                    b.Property<string>("InvestmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InvestmentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("InvestmentId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("Domain.Entities.InvestmentType", b =>
                {
                    b.Property<int>("InvestmentTypeId")
                        .HasColumnType("int");

                    b.Property<string>("InvestmentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvestmentTypeId");

                    b.ToTable("InvestmentTypes");
                });

            modelBuilder.Entity("Domain.Entities.Stock", b =>
                {
                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<decimal>("SharePrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("StockName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StockId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("Domain.Entities.StockInvestment", b =>
                {
                    b.Property<int>("InvestmentId")
                        .HasColumnType("int");

                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerShare")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PurchasedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SharesQuantity")
                        .HasColumnType("int");

                    b.HasKey("InvestmentId", "StockId");

                    b.ToTable("StockInvestments");
                });
#pragma warning restore 612, 618
        }
    }
}
