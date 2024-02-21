﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NintendoSwitchDeals.Common;

#nullable disable

namespace NintendoSwitchDeals.Common.Migrations
{
    [DbContext(typeof(DealsContext))]
    [Migration("20240221130553_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("NintendoSwitchDeals.Common.Models.Deal", b =>
                {
                    b.Property<int>("DealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<long>("NintendoId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ThresholdPrice")
                        .HasPrecision(6, 2)
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("DealId");

                    b.HasIndex("NintendoId")
                        .IsUnique();

                    b.ToTable("Deals");
                });
#pragma warning restore 612, 618
        }
    }
}
