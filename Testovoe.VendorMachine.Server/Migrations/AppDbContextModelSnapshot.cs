﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Testovoe.VendorMachine.Server.Data;

#nullable disable

namespace Testovoe.VendorMachine.Server.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Testovoe.VendorMachine.Server.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Coins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Count = 40,
                            IsBlocked = false,
                            Value = 1
                        },
                        new
                        {
                            Id = 2,
                            Count = 30,
                            IsBlocked = false,
                            Value = 2
                        },
                        new
                        {
                            Id = 3,
                            Count = 20,
                            IsBlocked = true,
                            Value = 5
                        },
                        new
                        {
                            Id = 4,
                            Count = 10,
                            IsBlocked = false,
                            Value = 10
                        });
                });

            modelBuilder.Entity("Testovoe.VendorMachine.Server.Models.Soda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sodas");
                });
#pragma warning restore 612, 618
        }
    }
}