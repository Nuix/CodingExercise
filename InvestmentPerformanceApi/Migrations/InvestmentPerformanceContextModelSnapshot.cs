﻿// <auto-generated />
using System;
using InvestmentPerformanceApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace InvestmentPerformanceApi.Migrations
{
    [DbContext(typeof(InvestmentPerformanceContext))]
    partial class InvestmentPerformanceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("InvestmentPerformanceApi.Models.InvestmentDetails", b =>
                {
                    b.Property<int>("InvestmentId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("CostBasis")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CurrentPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Shares")
                        .HasColumnType("int");

                    b.HasKey("InvestmentId", "CompanyName")
                        .HasName("ForeignKey_CompanyName");

                    b.ToTable("InvestmentDetails");
                });

            modelBuilder.Entity("InvestmentPerformanceApi.Models.Investments", b =>
                {
                    b.Property<int>("InvestmentId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvestmentId", "UserId")
                        .HasName("ForeignKey_UserId");

                    b.ToTable("Investments");
                });

            modelBuilder.Entity("InvestmentPerformanceApi.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId")
                        .HasName("PrimaryKey_UserId");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
